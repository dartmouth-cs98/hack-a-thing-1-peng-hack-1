using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{

    Animator animator;
    float power = 2.25f;
    public Transform bias;
    public Transform ball;
    public GameObject paddle;

    float VARIANCE = 1;
    float MOVE_SPEED = 2;

    Vector3 targetPos;
    public GameManager gameManager;
    public PlayerName playerName;


    // Start is called before the first frame update
    void Start()
    {
        animator = paddle.GetComponent<Animator>();
        targetPos = transform.position;   
    }

    // Update is called once per frame
    void Update()
    {
        targetPos.x = ball.position.x + (transform.position.x - paddle.transform.position.x);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, MOVE_SPEED * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collision) 
    {
        if (collision.CompareTag("ball")) {
            if (gameManager.ball.isLiveBall()) {
                gameManager.ball.lastHitter = playerName;
            }
            gameManager.ball.rallyLength += 1;
            gameManager.ball.numBounces = 0;
            Vector3 angle = bias.position - paddle.transform.position + new Vector3(Random.Range(-VARIANCE, VARIANCE), 0, Random.Range(-VARIANCE/2, VARIANCE/2));
            collision.GetComponent<Rigidbody>().velocity = angle.normalized * power  + new Vector3(0, 3.5f, 0);
            collision.GetComponent<Rigidbody>().useGravity = true;
            animator.Play("Swing");
        }
    }
}
