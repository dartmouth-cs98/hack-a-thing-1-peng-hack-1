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
    public bool isBeingReset = false;
    public GameObject hitCup;


    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // if Ball stops moving or if ball falls through floor.
        if (Vector3.Magnitude(transform.GetComponent<Rigidbody>().velocity) <= 1 ||
            transform.position.y < 0){
            StartCoroutine(gameManager.resetServe());
        }
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("floor"))
        {
            StartCoroutine(gameManager.resetServe());
        } else if (collision.transform.CompareTag("table"))
        {
            // reset if second bounce on table;
            if (hasHitTable) {
                StartCoroutine(gameManager.resetServe());
            } else {
                 // add bounciness if it hit the table
                hasHitTable = true;
                transform.GetComponent<Rigidbody>().velocity = transform.GetComponent<Rigidbody>().velocity + new Vector3(0, 1f, 0);
            }
        } else if (collision.transform.CompareTag("cup")) 
        {
            hitCup = collision.gameObject;
            hasHitCup = true;
        } else if (collision.transform.CompareTag("sink"))
        {
            hasSunk = true;
            hitCup = collision.gameObject;
        }
    }
}
