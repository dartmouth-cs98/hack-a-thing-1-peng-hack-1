using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    float player1Score = 7;
    float player2Score = 7;
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
        if (ball.hasSunk || (ball.hasHitCup && ball.hasHitTable)) {
            StartCoroutine(resetServe());
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

            ball.gameObject.transform.position = ball.startingPosition;
            ball.GetComponent<Rigidbody>().gameObject.SetActive(true);
            ball.GetComponent<Rigidbody>().velocity = Vector3.zero;

            ball.hasHitCup = false;
            ball.hasHitTable = false;
            ball.hasSunk = false;

            ball.isBeingReset = false;
        }


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
