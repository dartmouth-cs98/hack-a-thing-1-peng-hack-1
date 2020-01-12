using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform bias;
    public GameObject paddle;
    public float variance;
    float moveSpeed = 2f;
    float power = 2.25f;

    float BIAS_SPEED = 0.5f;
    float VARIANCE_RATE = 0.01f;
    float BIAS_SHIFT_RATE = 0.15f;
    float INITIAL_SIZE;
    Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        INITIAL_SIZE = variance;   
        animator = paddle.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Player movement 
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (horizontal != 0 || vertical != 0)
        {
            transform.Translate(new Vector3(horizontal, 0, vertical) * moveSpeed * Time.deltaTime);
            variance += VARIANCE_RATE;
            bias.gameObject.transform.localScale += new Vector3(VARIANCE_RATE, 0, VARIANCE_RATE);
            bias.gameObject.transform.Translate(new Vector3(horizontal , 0, vertical) * moveSpeed * Time.deltaTime * BIAS_SHIFT_RATE);
        }

        // If player isn't moving, decrease variance.
        if (horizontal == 0 && vertical == 0) {
            if (bias.gameObject.transform.localScale.x > INITIAL_SIZE) {
                variance -= VARIANCE_RATE;
                bias.gameObject.transform.localScale += new Vector3(-VARIANCE_RATE * 1.15f, 0, -VARIANCE_RATE * 1.15f);
            } else {
                variance = INITIAL_SIZE;
                bias.gameObject.transform.localScale = new Vector3(INITIAL_SIZE, -0.001f, INITIAL_SIZE);
            }
        }

        if (Input.GetKey(KeyCode.Z)) 
        {
            bias.Translate(new Vector3(-BIAS_SPEED, 0, 0) * moveSpeed * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.X))
        {
            bias.Translate(new Vector3(BIAS_SPEED, 0, 0) * moveSpeed * Time.deltaTime);
        }
    }


    private void OnTriggerEnter(Collider collision) 
    {
        if (collision.CompareTag("ball")) {
            Vector3 angle = bias.position - paddle.transform.position + new Vector3(Random.Range(-variance, variance), 0, Random.Range(-variance, variance));
            collision.GetComponent<Rigidbody>().velocity = angle.normalized * power  + new Vector3(0, 4, 0);
            animator.Play("Swing");
        }
    }
}
