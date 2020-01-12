using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrub : MonoBehaviour
{
    Cup[] cups;
    public GameObject[] placeHolders;
    public GameObject cup;
    public Dictionary<string, Cup> idToCup = new Dictionary<string, Cup>();
    public int shrubNumber;
    
    public GameManager gameManager;

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
            GameObject clone = Instantiate(cup, placeHolders[i].gameObject.transform.position, Quaternion.identity);
            Cup newCup = clone.GetComponent<Cup>();

            string id = shrubNumber + "-" + i;
            newCup.cupId = id;
            newCup.remainingLiquid = 1.0f;
            newCup.gameManager = gameManager;
            newCup.sinkTarget.gameManager = gameManager;

            idToCup.Add(id, newCup);
            placeHolders[i].SetActive(false);
        }
    }
}