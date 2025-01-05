using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private Transform GFX;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform feetPos;
    [SerializeField] private float groundDistance = 0.25f;
    [SerializeField] private Animator animator;

    private bool isGrounded = false;
    private int jumpCount = 0; // 현재 점프 횟수
    private const int maxJumps = 2; // 최대 점프 횟수

    private void Update()
    {
        // 바닥 감지
        isGrounded = Physics2D.OverlapCircle(feetPos.position, groundDistance, groundLayer);

        // 바닥에 닿았을 때 점프 횟수 초기화
        if (isGrounded)
        {
            jumpCount = 0;
            animator.SetBool("IsJumping", false);
        }

        // 점프 입력 처리
        if (Input.GetButtonDown("Jump") && jumpCount < maxJumps)
        {
            Jump();
        }

        // 애니메이션 상태 업데이트
        if (!isGrounded && jumpCount > 0)
        {
            animator.SetBool("IsJumping", true);
        }
    }

    private void Jump()
    {
        jumpCount++; // 점프 횟수 증가
        rb.velocity = new Vector2(rb.velocity.x, 0); // 기존 Y축 속도 초기화
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // 점프 힘 적용
    }
}
