using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private static float health = 100;   // ���� ü�� (static���� ����)
    public float maxHealth = 100;       // �ִ� ü��
    [SerializeField]
    private Slider healthBarSlider;     // HealthBar UI (Slider)

    public AudioSource audioSource;     // �浹 �� ����� �����

    private Animator animator; //�ִϸ��̼�
    private bool IsDead; //�׾���?

    void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        animator = GetComponent<Animator>();

        // ü���� �ִ밪���� ũ�� �ִ밪���� ����
        health = Mathf.Clamp(health, 0, maxHealth);

        UpdateHealthbar();

        IsDead = false; //�������� �� ����
    }

    private void UpdateHealthbar()
    {
        // Slider�� �� ������Ʈ
        healthBarSlider.value = health / maxHealth;
    }

    public void TakeDamage(float damageAmount)
    {

        if (IsDead) return; // �̹� �׾����� �� �̻� ������ ó������ ����
        animator.SetTrigger("IsDamaged"); // ������ ��� ���

        // ü�� ����
        health -= damageAmount;
        health = Mathf.Clamp(health, 0, maxHealth); // ü�� ���� ����
        UpdateHealthbar();

        // ü�� 0�� �Ǹ� ó��
        if (health <= 0)
        {
            DeathSequence();
            OnHealthDepleted();
        }

        // ����� ���
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
        // Animator���� ���� ������ �ִϸ��̼� ���̸� ������
        RuntimeAnimatorController ac = animator.runtimeAnimatorController;

        foreach (AnimationClip clip in ac.animationClips)
        {
            if (clip.name == clipName)
            {
                return clip.length;
            }
        }

        // �ִϸ��̼� Ŭ���� ���� ��� �⺻ ��� �ð� ��ȯ
        Debug.LogWarning($"Animation clip '{clipName}' not found!");
        return 0f;
    }

    private void LoadLoseScene()
    {
        SceneManager.LoadScene("LoseScene");
    }

    public void SetHealth(float healthAmount)
    {
        // ü�� ����
        health = Mathf.Clamp(healthAmount, 0, maxHealth);
        UpdateHealthbar();
    }

    public float GetHealth()
    {
        return health;
    }

    private void OnHealthDepleted()
    {

        // ü���� 0�� �Ǿ��� �� LoseScene���� �̵�
        ResetHealth(); // ���� ���� �� ü���� �ʱ�ȭ
        //SceneManager.LoadScene("LoseScene");
    }

    private void ResetHealth()
    {
        // ü�� �ʱ�ȭ (�� ������ ������ ����)
        health = maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���� �浹 �� ü�� ����
        if (collision.CompareTag("Enemy"))
        {
            TakeDamage(10); // Enemy�� 10�� ������
        }
        else if (collision.CompareTag("Obstacle"))
        {
            TakeDamage(5); // Obstacle�� 5�� ������
        }
    }
}
