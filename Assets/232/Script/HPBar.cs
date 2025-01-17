using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private static float health = 100;   // 현재 체력 (static으로 설정)
    public float maxHealth = 100;       // 최대 체력
    [SerializeField]
    private Slider healthBarSlider;     // HealthBar UI (Slider)

    public AudioSource audioSource;     // 충돌 시 재생될 오디오

    private Animator animator; // damaged, dead motion

    void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        animator = GetComponent<Animator>();

        // 체력이 최대값보다 크면 최대값으로 제한
        health = Mathf.Clamp(health, 0, maxHealth);

        UpdateHealthbar();
    }

    private void UpdateHealthbar()
    {
        // Slider의 값 업데이트
        healthBarSlider.value = health / maxHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        animator.SetTrigger("IsDamaged"); // damaged motion 재생

        // 체력 감소
        health -= damageAmount;
        health = Mathf.Clamp(health, 0, maxHealth); // 체력 범위 제한
        UpdateHealthbar();

        // 체력 0이 되면 처리
        if (health <= 0)
        {
            Death();
        }

        // 오디오 재생
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    public void SetHealth(float healthAmount)
    {
        // 체력 설정
        health = Mathf.Clamp(healthAmount, 0, maxHealth);
        UpdateHealthbar();
    }

    public float GetHealth()
    {
        return health;
    }

    private void Death()
    {
        animator.SetBool("IsDead", true); // dead motion 재생
        StartCoroutine(WaitForDeath()); // 덕칠이 죽는 모션부터 씬 넘어가는 것까지
    }

    private IEnumerator WaitForDeath()
    {
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("Dead"))
        {
            yield return null; // Dead motion 재생 될 때까지 대기
        }

        // Dead motion 끝까지 대기
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }

        // 죽는 모션 후 루즈씬 넘어가기 전 1초 대기
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("LoseScene");
        ResetHealth(); // 게임 오버 시 체력을 초기화
    }

    private void ResetHealth()
    {
        // 체력 초기화 (새 게임을 시작할 때만)
        health = maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 적과 충돌 시 체력 감소
        if (collision.CompareTag("Enemy"))
        {
            TakeDamage(10); // Enemy는 10씩 데미지
        }
        else if (collision.CompareTag("Obstacle"))
        {
            TakeDamage(5); // Obstacle은 5씩 데미지
        }
    }
}
