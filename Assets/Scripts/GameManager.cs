using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    float player1Score = 7;
    float player2Score = 7;
    public Ball ball;
    public Shrub shrub1;
    public Shrub shrub2;


    // Start is called before the first frame update
    void Start()
    {
        spawnCups();
    }

    // Update is called once per frame
    void Update()
    {
        if (ball.hasHitCup && ball.hasHitTable) {
            resetServe();
        }
    }

    void spawnCups() 
    {
        shrub1.spawn();
        shrub2.spawn();
    }

    void resetServe()
    {

    }
}
