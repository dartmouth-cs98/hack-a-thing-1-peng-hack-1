using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class responsible for spawning and keeping track of the active/inactive cups
/// </summary>
public class Shrub : MonoBehaviour
{
    Cup[] cups;
    public GameObject[] placeHolders;
    public GameObject cup;
    public Dictionary<string, Cup> idToCup = new Dictionary<string, Cup>();
    public int shrubNumber;
    public GameManager gameManager;

    void Start()
    {
        cups = new Cup[placeHolders.Length];
    }

    /// <summary>
    /// Spawns cups and replaces placeholders.
    /// stores id's to cups in a dictionary;
    /// </summary>
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