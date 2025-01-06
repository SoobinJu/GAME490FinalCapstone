using System;
using UnityEngine;

namespace MazeTemplate
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody2D rb;
        private float speed = 10f;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = 0;
        }

        private void Update()
        {
            HandleMovement();
        }

        private void HandleMovement()
        {
            Vector2 movement = Vector2.zero;

            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                movement += Vector2.up;
            }
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                movement += Vector2.down;
            }
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                movement += Vector2.left;
            }
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                movement += Vector2.right;
            }

            if (movement != Vector2.zero)
            {
                rb.velocity = movement.normalized * speed;
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Win"))
            {
                Destroy(gameObject, 3);
            }
        }
    }
}
