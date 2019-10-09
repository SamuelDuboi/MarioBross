using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnnemyBehavior : MonoBehaviour
{
    public float speed;
    public Rigidbody2D ennemie;
    public BoxCollider2D box;
     Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ennemie.velocity = new Vector2(-speed, ennemie.velocity.y);
    }

    private void Update()
    {
        if (transform.position.y < -6)
            Destroy(gameObject);

        Collider2D collider = Physics2D.OverlapBox(new Vector2(transform.position.x, transform.position.y + 0.5f), new Vector2(0.9f, 0.2f), 0);
        if (collider.CompareTag("Player"))
        {
            StartCoroutine(Death());
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Pipe")
            speed = -speed;
         
   
    }



    IEnumerator Death()
    {
        // transition d'animation a faire
        animator.SetBool("death", true);
        speed = 0;
        box.isTrigger = true;
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    void KillBoop()
    {
        transform.localScale = -transform.localScale;
        speed = -5;
        ennemie.velocity = new Vector2(speed, 16);
        box.isTrigger = true;
       
    }

}
