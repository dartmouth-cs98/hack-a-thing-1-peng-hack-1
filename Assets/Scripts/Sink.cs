using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sink : MonoBehaviour
{
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
        Debug.Log("here");
        if (collider.CompareTag("ball")) {
            collider.GetComponent<Rigidbody>().velocity = Vector3.zero;
            collider.transform.position = collider.transform.position + new Vector3(0,-0.001f, 0);
        }
    }
}
