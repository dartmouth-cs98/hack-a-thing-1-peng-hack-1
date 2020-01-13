using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour
{

    public Sink sinkTarget;
    public string cupId;
    public float remainingLiquid = 1.0f;
    public GameManager gameManager;
    public bool isSunk;

    public Cup (string id, GameManager gameManager)
    {
        this.cupId = id;
        this.gameManager = gameManager;
    }

    // Update is called once per frame
    void Update()
    {
        if (remainingLiquid <= 0) {
            StartCoroutine(gameManager.removeCup(cupId, 0.5f));
        }
    }

    void hit() {
        // mark that cup has been hit. There's still a chance that ball will be saved. 
        gameManager.ball.hasHitCup = true;
        gameManager.ball.hitCup = this;
    }

    public void sink() {
        // a sink is a sink
        StartCoroutine(gameManager.removeCup(cupId, remainingLiquid));
        remainingLiquid = 0;
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.transform.CompareTag("ball") && !gameManager.ball.hasHitCup) {
            Debug.Log("Hit cup " + cupId + " remaining liquid: " + remainingLiquid);

            if (isSunk) {
                sink();
            }

            if (!gameManager.ball.hasSunk && !gameManager.ball.hasHitCup) {
                hit();
            }
        }
    }
}
