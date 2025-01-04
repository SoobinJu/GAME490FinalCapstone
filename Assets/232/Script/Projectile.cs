using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed;
    public Rigidbody2D rb;
    float timer;

    private void Start()
    {
        rb.velocity = transform.right * speed;
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >=10)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.CompareTag("Player"))
        {
            Destroy(gameObject);

        }
    }

}
