using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour
{

    GameObject sinkTarget;
    int cupNumber;
    float remainingLiquid = 1;

    public Cup (int id)
    {
        this.cupNumber = id;
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
        remainingLiquid -= 0.5f;
    }

    void sink() {
        remainingLiquid = 0;
    }

    void OnCollisionEnter() {
        hit();
    }
}
