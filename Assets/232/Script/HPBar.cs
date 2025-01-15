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

    private Animator animator; //애니메이션
    private bool IsDead; //죽었냐?

    void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        animator = GetComponent<Animator>();

        // 체력이 최대값보다 크면 최대값으로 제한
        health = Mathf.Clamp(health, 0, maxHealth);

        UpdateHealthbar();

        IsDead = false; //ㄴㄴ아직 안 죽음
    }

    private void UpdateHealthbar()
    {
        // Slider의 값 업데이트
        healthBarSlider.value = health / maxHealth;
    }

    public void TakeDamage(float damageAmount)
    {

        if (IsDead) return; // 이미 죽었으면 더 이상 데미지 처리하지 않음
        animator.SetTrigger("IsDamaged"); // 데미지 모션 재생

        // 체력 감소
        health -= damageAmount;
        health = Mathf.Clamp(health, 0, maxHealth); // 체력 범위 제한
        UpdateHealthbar();

        // 체력 0이 되면 처리
        if (health <= 0)
        {
            DeathSequence();
            OnHealthDepleted();
        }

        // 오디오 재생
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    private void DeathSequence()
    {
        IsDead = true;
        animator.SetBool("IsDead", true);
        Invoke("LoadLoseScene", GetAnimationClipLength("Dead"));
    }

    private float GetAnimationClipLength(string clipName)
    {
        // Animator에서 현재 상태의 애니메이션 길이를 가져옴
        RuntimeAnimatorController ac = animator.runtimeAnimatorController;

        foreach (AnimationClip clip in ac.animationClips)
        {
            if (clip.name == clipName)
            {
                return clip.length;
            }
        }

        // 애니메이션 클립이 없는 경우 기본 대기 시간 반환
        Debug.LogWarning($"Animation clip '{clipName}' not found!");
        return 0f;
    }

    private void LoadLoseScene()
    {
        SceneManager.LoadScene("LoseScene");
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

    private void OnHealthDepleted()
    {

        // 체력이 0이 되었을 때 LoseScene으로 이동
        ResetHealth(); // 게임 오버 시 체력을 초기화
        //SceneManager.LoadScene("LoseScene");
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
