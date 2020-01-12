using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrub : MonoBehaviour
{
    Cup[] cups;
    public GameObject[] placeHolders;
    public GameObject cup;

    // Start is called before the first frame update
    void Start()
    {
        cups = new Cup[placeHolders.Length];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawn()
    {
        for (int i = 0; i < placeHolders.Length; i++) {
            Instantiate(cup, placeHolders[i].gameObject.transform.position, Quaternion.identity);
            placeHolders[i].SetActive(false);

        }
    }
}