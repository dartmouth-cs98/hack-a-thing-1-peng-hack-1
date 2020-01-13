using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// If ball collides with sink target, it is considered a sink
/// </summary>
public class Sink : MonoBehaviour
{
    public Cup cup;
    public GameManager gameManager;

    void OnTriggerEnter(Collider collider) 
    {
        if (collider.CompareTag("ball")) {
            Debug.Log("Sink Cup");

            // prevent ball from moving around.
            collider.GetComponent<Rigidbody>().velocity = Vector3.zero;
            collider.GetComponent<Rigidbody>().gameObject.SetActive(false);

            cup.isSunk = true;
            gameManager.ball.hasSunk = true;
            gameManager.ball.lastHitter = gameManager.ball.lastHitter == PlayerName.One ? PlayerName.Two : PlayerName.One;

            cup.sink();
        }
    }
}
