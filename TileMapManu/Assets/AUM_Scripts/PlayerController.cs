using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public float acceleration = 1f;
    public float force = 100f;
    bool canJump = true;
    public float maxSpeed = 10f;
     public static Rigidbody2D playerrbg;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        playerrbg = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        if ((inputX > 0) && (speed < maxSpeed))
        {
            speed += acceleration;

        }
        else if((inputX < 0) && (speed > -maxSpeed))
        {
            speed -= acceleration;
        }

        else if(!Mathf.Approximately(0, speed))
        {
            if(speed > 0)
            { 
                speed -= acceleration;
                if(speed < 0)
                {
                    speed = 0;
                }
            }
            else if (speed < 0)
            {
                speed += acceleration;
                if (speed > 0)
                {
                    speed = 0;
                }
            }
        }


        playerrbg.velocity = new Vector2(speed, playerrbg.velocity.y);
        if (playerrbg.velocity.x != 0)
        {
            animator.SetBool("IsRunning", true);
        }
        else
            animator.SetBool("IsRunning", false);


        if (Input.GetKeyDown("space") && canJump)
        {
            //transform.Translate(Vector2.up * 260 * Time.deltaTime, Space.World);

            canJump = false;

            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 12), ForceMode2D.Impulse);
                if (playerrbg.velocity.x == 0)
                    animator.SetBool("IsJumping", true);
                else
                    animator.SetBool("IsJumpingRunning", true);
        }


        

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "sol" || collision.transform.tag == "Pipe")
        {
            canJump = true;
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsJumpingRunning", false);
        }
    }
    private void Update()
    {
        if (transform.position.y <= -6)
            SceneManager.LoadScene("AUM_Niveau_2");
    }
}
