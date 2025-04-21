using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using static UnityEngine.Rendering.DebugUI;
using Unity.VisualScripting;

namespace MazeTemplate
{
    public class MazePlayer : MonoBehaviour
    {
        private Rigidbody2D rb;
        private float speed = 10f;

        private Animator animator;
        private SpriteRenderer spriteRenderer;

        private Vector2 movement;

        public Gumiho_mini gumiho;
        public new L2Light light;
        public Mini_TimeLimit time;
        public PlayerHealth health;

        public GameObject goalPanel;

        private bool isFinished = false;
        private bool isDead = false;

        AudioSource audioSource;
        public AudioClip runSound;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = 0;

            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            audioSource = gameObject.AddComponent<AudioSource>();

            goalPanel.SetActive(false);
        }

        private void Update()
        {
            HandleMovement();

            if (!isDead && gumiho.isWatching && movement != Vector2.zero)
            {
                isDead = true;

                rb.velocity = Vector2.zero;
                rb.constraints = RigidbodyConstraints2D.FreezeAll;

                time.TimePause();
                health.Death();
            }
        }

        private void HandleMovement()
        {
            movement = Vector2.zero;

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

            if (!isFinished && collision.CompareTag("Goal"))
            {
                isFinished = true;
                
                StartCoroutine(Finish());
            }
            if (collision.CompareTag("Trigger"))
            {
                SceneManager.LoadScene("Red_Green");
            }
        }

        IEnumerator Finish()
        {
            rb.velocity = Vector2.zero;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;

            gumiho.StopWatching();
            time.TimePause();

            goalPanel.SetActive(true);
            yield return new WaitForSeconds(3f);

            SceneManager.LoadScene("Game3");
            L2Light.maxUses++;
            light.UpdateGUI();
        }

        public void RunningSound()
        {
            audioSource.PlayOneShot(runSound);
        }
    }
}
