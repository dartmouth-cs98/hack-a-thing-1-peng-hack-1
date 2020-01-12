using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sink : MonoBehaviour
{
    public Cup cup;
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider) 
    {
        if (collider.CompareTag("ball")) {
            Debug.Log("Sink Cup");
            collider.GetComponent<Rigidbody>().velocity = Vector3.zero;
            collider.GetComponent<Rigidbody>().gameObject.SetActive(false);
            cup.isSunk = true;
            gameManager.ball.hasSunk = true;

            cup.sink();
        }
    }
}
