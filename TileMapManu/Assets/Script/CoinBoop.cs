using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBoop : MonoBehaviour
{
    public float speed;
    public float amplitude;
    public float timeBeforeKill;

    private float startYPos;
    private float times;
    private void Start()
    {
        times = 0;
        startYPos = transform.position.y;
        Invoke("Kill", timeBeforeKill);
    }
    void Update()
    {
        times += Time.deltaTime;
        transform.position = new Vector2(transform.position.x, Mathf.Sin(times * speed) * amplitude);
    }

    void Kill()
    {
        Destroy(gameObject);
    }
}
