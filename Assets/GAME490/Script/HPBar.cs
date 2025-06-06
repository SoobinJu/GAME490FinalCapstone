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

    AudioSource audioSource;     // 충돌 시 재생될 오디오
    public AudioClip DamagedSound;
    public AudioClip DeadSound;

    private Animator animator; // damaged, dead motion
    private Rigidbody2D rb;

    void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        // 1. PlayerPrefs에서 저장된 체력 불러오기
        if (PlayerPrefs.HasKey("PlayerHealth"))
        {
            health = PlayerPrefs.GetFloat("PlayerHealth");
        }

        // 2. Game3에서 시작한 경우
        if (SceneManager.GetActiveScene().name == "Game3")
        {
            // 미니게임에서 돌아온 경우가 아니라면 체력을 max로 설정
            if (PlayerPrefs.GetInt("ReturningFromMiniGame", 0) == 0)
            {
                health = maxHealth;
            }
            // 돌아온 경우에는 PlayerPrefs에서 불러온 체력 사용

            // 플래그 초기화 (다시 돌아왔다고 판단했으니)
            PlayerPrefs.SetInt("ReturningFromMiniGame", 0);
            PlayerPrefs.Save();
        }

        // 체력값을 최대/최소 범위에 맞게 조정
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
            rb.velocity = Vector2.zero;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            
            Death();
        }

        // 오디오 재생
        if (audioSource != null)
        {
            audioSource.PlayOneShot(DamagedSound);
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

    public void Death()
    {
        animator.SetBool("IsDead", true); // dead motion 재생
        StartCoroutine(WaitForDeath()); // 덕칠이 죽는 모션부터 씬 넘어가는 것까지
        GameProgressTracker.Instance?.ResetProgress();
        audioSource.PlayOneShot(DeadSound);
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

    public void ResetHealth()
    {
        health = maxHealth;
        UpdateHealthbar();
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
        else if (collision.CompareTag("Respawn"))
        {
            TakeDamage(100); // killzone은 완전히
        }
    }
}
