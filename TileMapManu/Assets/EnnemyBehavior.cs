using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyBehavior : MonoBehaviour
{
    public float speed;
    public Rigidbody2D ennemie;
    public BoxCollider2D box;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ennemie.velocity = new Vector2(-speed, ennemie.velocity.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Pipe")
            speed = -speed;

        if (collision.gameObject.tag == "Player")
            if (collision.contacts[0].point.x > transform.position.x - 0.1f && collision.contacts[0].point.x < transform.position.x + 0.1f)
            {
                StartCoroutine(Death());

            }
    }

    IEnumerator Death()
    {

        yield return new WaitForSeconds(1);
    }

    void KillBoop()
    {
        transform.localScale = -transform.localScale;
        speed = -1;
        box.isTrigger = true;

        if (transform.position.y < -6)
            Destroy(gameObject);
    }

}
