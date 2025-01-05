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
    private int jumpCount = 0; // ���� ���� Ƚ��
    private const int maxJumps = 2; // �ִ� ���� Ƚ��

    private void Update()
    {
        // �ٴ� ����
        isGrounded = Physics2D.OverlapCircle(feetPos.position, groundDistance, groundLayer);

        // �ٴڿ� ����� �� ���� Ƚ�� �ʱ�ȭ
        if (isGrounded)
        {
            jumpCount = 0;
            animator.SetBool("IsJumping", false);
        }

        // ���� �Է� ó��
        if (Input.GetButtonDown("Jump") && jumpCount < maxJumps)
        {
            Jump();
        }

        // �ִϸ��̼� ���� ������Ʈ
        if (!isGrounded && jumpCount > 0)
        {
            animator.SetBool("IsJumping", true);
        }
    }

    private void Jump()
    {
        jumpCount++; // ���� Ƚ�� ����
        rb.velocity = new Vector2(rb.velocity.x, 0); // ���� Y�� �ӵ� �ʱ�ȭ
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // ���� �� ����
    }
}
