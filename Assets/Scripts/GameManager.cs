using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    float player1Score = 7;
    float player2Score = 7;

    public Transform player1ServeLocation;
    public Transform player2ServeLocation;
    public Ball ball;
    public Shrub shrub1;
    public Shrub shrub2;


    // Start is called before the first frame update
    void Start()
    {
        spawnCups();
    }

    // Update is called once per frame
    void Update()
    {
        if (player1Score > 0 && player2Score > 0) {
            if (ball.hasSunk || (ball.hasHitCup && ball.hasHitTable)) {
                StartCoroutine(resetServe());
            }
        }

    }

    void spawnCups() 
    {
        shrub1.spawn();
        shrub2.spawn();
    }

    public IEnumerator resetServe()
    {
        if (!ball.isBeingReset)
        {
            Debug.Log("resetting serve");
            ball.isBeingReset = true;

            yield return new WaitForSeconds(3);

            ball.gameObject.transform.position = isPlayerOneServe() ? player1ServeLocation.position : player2ServeLocation.position;
            ball.GetComponent<Rigidbody>().gameObject.SetActive(true);
            ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
            ball.GetComponent<Rigidbody>().useGravity = false;

            ball.hasHitCup = false;
            ball.hasHitTable = false;
            ball.hasSunk = false;
            ball.hasHitFloor = false;
            ball.rallyLength = 0;
            ball.numBounces = 0;
            ball.hitCup = null;

            ball.isBeingReset = false;
        }
    }

    bool isPlayerOneServe()
    {
        Debug.Log(ball.hitCup);
        if (ball.hitCup != null) {
            // if resetting because of a cup that was hit, the person's cup who was hit serves.
            return shrub1.idToCup.ContainsKey(ball.hitCup.cupId);
        }
        return ball.lastHitter == PlayerName.One;
    }


    public IEnumerator removeCup(string id, float remainingLiquid) 
    {
        yield return new WaitForSeconds(1);
        Debug.Log("remove Cup " + id + " " + shrub2.idToCup.ContainsKey(id));
        if (shrub1.idToCup.ContainsKey(id))
        {
            Destroy(shrub1.idToCup[id], 3);
            shrub1.idToCup[id].gameObject.SetActive(false);
            player1Score -= remainingLiquid;
        } else if (shrub2.idToCup.ContainsKey(id)) 
        {
            Destroy(shrub2.idToCup[id], 3);
            shrub2.idToCup[id].gameObject.SetActive(false);
            player2Score -= remainingLiquid;
        }
    }
}
