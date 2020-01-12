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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void hit() {
        gameManager.ball.hasHitCup = true;
        remainingLiquid -= 0.5f;
        if (remainingLiquid <= 0) {
            StartCoroutine(gameManager.removeCup(cupId, 0.5f));
        }
    }

    public void sink() {
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
