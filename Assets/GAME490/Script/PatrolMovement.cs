using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolMovement : MonoBehaviour
{
    public List<Transform> patrolPoints;
    int patrolIndex = 0;
    public float moveSpeed;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    AudioSource audioSource;
    public AudioClip attackSound;

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    void Update()
    {
        Move();
        Flip();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 적과 충돌 시 공격 모션 재생
        if (collision.CompareTag("Player"))
        {
            animator.SetBool("IsAttacking", true);
            audioSource.PlayOneShot(attackSound);
        }
    }

    private void Move()
    {
        Vector2 targetPosition = patrolPoints[patrolIndex].position;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, Time.deltaTime * moveSpeed);

        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            patrolIndex++;
            if (patrolIndex >= patrolPoints.Count)
            {
                patrolIndex = 0;
            }
        }
    }

    private void Flip()
    {
        if (patrolPoints.Count > 0)
        {
            float direction = patrolPoints[patrolIndex].position.x - transform.position.x;

            if (direction > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);  // Face Right
            }
            else if (direction < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1); // Face Left (flipped)
            }
        }
    }

}
