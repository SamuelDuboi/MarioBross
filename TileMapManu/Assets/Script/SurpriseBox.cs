using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurpriseBox : MonoBehaviour
{
    public bool destroyable;
    public GameObject destroyParticleO;
    public Sprite solidBlockSprite;
    public GameObject storedItem;
    [Range(0,20)]
    public int storedCoins;
    public GameObject coinAnimationO;
    public float riseSpeed;

    Collider2D hitCollider;
    bool canBeBooped;
    bool itemInside;
    bool isBooping;
    void Start()
    {
        isBooping = false;

        if(storedItem != null)
        {
            itemInside = true;
        }
        else
        {
            itemInside = false;
        }

        canBeBooped = true;
    }

    void Update()
    {
        if(canBeBooped && !isBooping)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                StartCoroutine(Boop());
            }

            hitCollider = Physics2D.OverlapBox(new Vector2(transform.position.x, transform.position.y - 0.5f), new Vector2(0.9f, 0.2f), 0.0f);
            if (hitCollider.CompareTag("Player"))
            {
                StartCoroutine(Boop());
            }

            if (storedCoins <= 0 && !itemInside)
            {
                canBeBooped = false;
                GetComponent<SpriteRenderer>().sprite = solidBlockSprite;
            }
        }
    }

    private IEnumerator Boop()
    {
        isBooping = true;
        float startYPos = transform.position.y;
        float relativeTime = 0;
        Collider2D aboveCollider = Physics2D.OverlapBox(new Vector2(transform.position.x, transform.position.y + 0.6f), new Vector2(1.0f, 0.1f), 0.0f);
        if(aboveCollider != null && aboveCollider.CompareTag("ennemy"))
        {
            aboveCollider.gameObject.GetComponent<EnnemyBehavior>().KillBoop();
        }

        if (storedCoins > 0)
        {
            storedCoins--;
            Instantiate(coinAnimationO, transform.position, Quaternion.identity);
        }

        if(!destroyable)
        {
            while (transform.position.y >= startYPos)
            {
                transform.position = new Vector2(transform.position.x, startYPos + Mathf.Sin(relativeTime * 20) * 0.5f);
                relativeTime += 0.02f;
                yield return new WaitForSeconds(0.02f);
            }

            transform.position = new Vector2(transform.position.x, startYPos);

            if (storedItem != null && itemInside)
            {
                itemInside = false;
                StartCoroutine(RiseItem(Instantiate(storedItem, new Vector2(transform.position.x, transform.position.y), Quaternion.identity)));

            }
        }
        else
        {
            Instantiate(destroyParticleO, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        isBooping = false;
    }

    private IEnumerator RiseItem(GameObject item)
    {
        float relativeYPos = 0;
        float startYPos = item.transform.position.y;
        while(relativeYPos < 1.0f)
        {
            item.transform.position = new Vector2(transform.position.x, startYPos + relativeYPos);
            relativeYPos += 0.02f * riseSpeed;
            yield return new WaitForSeconds(0.02f);
        }

        item.transform.position = new Vector2(transform.position.x, transform.position.y + 1.0f);
    }
}
