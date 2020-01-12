using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    GameManager gameManager;
    Vector3 startingPosition;
    public bool hasHitTable = false;
    public bool hasHitCup = false;
    public GameObject hitCup;


    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("floor"))
        {
            transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.position = startingPosition;
        } else if (collision.transform.CompareTag("table"))
        {
            hasHitTable = true;
            transform.GetComponent<Rigidbody>().velocity = transform.GetComponent<Rigidbody>().velocity + new Vector3(0, 1f, 0);
        } else if (collision.transform.CompareTag("cup")) 
        {
            hitCup = collision.gameObject;
            hasHitCup = true;
        }
    }
}
