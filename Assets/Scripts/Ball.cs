using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public GameManager gameManager;
    public Vector3 startingPosition;
    public bool hasHitTable = false;
    public bool hasHitCup = false;
    public bool hasSunk = false;
    public bool hasHitFloor = false;
    public bool isBeingReset = false;
    public Cup hitCup;
    public PlayerName lastHitter;
    public int numBounces = 0;
    public int rallyLength = 0;


    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasHitCup && !isLiveBall()) {
            lastHitter = lastHitter == PlayerName.One ? PlayerName.One : PlayerName.Two;
        }
    }

    public bool isLiveBall()
    {
        if (hasSunk || hasHitFloor || isBeingReset || numBounces >= 2) {
            return false;
        }
        return true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("floor"))
        {
            
            if (hasHitCup) {
                hitCup.remainingLiquid -= 0.5f;
            }

            Debug.Log("reset because off table");
            if (numBounces == 1 && isLiveBall()) {  // If a player whiffs the shot, they have to serve. 
                lastHitter = lastHitter == PlayerName.One ? PlayerName.Two : PlayerName.One;
            }
            hasHitFloor = true;
            StartCoroutine(gameManager.resetServe());
        } else if (collision.transform.CompareTag("table"))
        {
            numBounces += 1;
            // reset if second bounce on table;
            if (numBounces >= 2) {

                if (hasHitCup) {
                    hitCup.remainingLiquid -= 0.5f;
                }

                Debug.Log("Reset second bounce");
                StartCoroutine(gameManager.resetServe());
            } else {
                 // add bounciness if hit the table
                hasHitTable = true;
                hasHitCup = false;
                transform.GetComponent<Rigidbody>().velocity = transform.GetComponent<Rigidbody>().velocity + new Vector3(0, 1.25f, 0);
            }
        } else if (collision.transform.CompareTag("cup")) 
        {
            if (rallyLength >= 1) {      // can't serve at cup
                Debug.Log("here");
                hasHitCup = true;
                hitCup = collision.gameObject.GetComponent<Cup>();
            }
        } else if (collision.transform.CompareTag("sink"))
        {
            if (rallyLength >= 1) {      // Can't serve at cup
                hasSunk = true;
                hitCup = collision.gameObject.GetComponent<Cup>();
            }
        }
    }
}
