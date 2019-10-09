using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public float acceleration = 0.1f;
    public float force = 100f;
    bool canJump = true;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        Vector2 position = transform.position;

        if ((inputX > 0) && (speed < 0.2))
        {
            speed += acceleration;

        }
        else if((inputX < 0) && (speed > -0.2))
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

        position.x += speed;

        transform.position = position;


        if (Input.GetKeyDown("space") && canJump)
        {
            //transform.Translate(Vector2.up * 260 * Time.deltaTime, Space.World);

            canJump = false;

            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
        }

        

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision");

        if(collision.transform.tag == "sol")
        {
            canJump = true;
        }
    }
}
