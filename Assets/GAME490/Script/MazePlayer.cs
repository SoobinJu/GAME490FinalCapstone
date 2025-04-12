using System;
using UnityEngine;

namespace MazeTemplate
{
    public class MazePlayer : MonoBehaviour
    {
        private Rigidbody2D rb;
        private float speed = 10f;

        private Animator animator;
        private SpriteRenderer spriteRenderer;

        AudioSource audioSource;
        public AudioClip RunSound;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = 0;

            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            audioSource = gameObject.AddComponent<AudioSource>();
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
                spriteRenderer.flipX = true; // 왼쪽으로 뒤집기
            }
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                movement += Vector2.right;
                spriteRenderer.flipX = false; // 오른쪽으로 뒤집기
            }

            if (movement != Vector2.zero)
            {
                animator.SetBool("IsRunning", true);
                rb.velocity = movement.normalized * speed;
            }
            else
            {
                animator.SetBool("IsRunning", false);
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

        public void RunningSound()
        {
            audioSource.PlayOneShot(RunSound);
        }
    }
}
