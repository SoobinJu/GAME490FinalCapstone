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

    private Animator animator; // damaged, dead motion

    void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        animator = GetComponent<Animator>();

        // ü���� �ִ밪���� ũ�� �ִ밪���� ����
        health = Mathf.Clamp(health, 0, maxHealth);

        UpdateHealthbar();
    }

    private void UpdateHealthbar()
    {
        // Slider�� �� ������Ʈ
        healthBarSlider.value = health / maxHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        animator.SetTrigger("IsDamaged"); // damaged motion ���

        // ü�� ����
        health -= damageAmount;
        health = Mathf.Clamp(health, 0, maxHealth); // ü�� ���� ����
        UpdateHealthbar();

        // ü�� 0�� �Ǹ� ó��
        if (health <= 0)
        {
            Death();
        }

        // ����� ���
        if (audioSource != null)
        {
            audioSource.Play();
        }
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

    private void Death()
    {
        animator.SetBool("IsDead", true); // dead motion ���
        StartCoroutine(WaitForDeath()); // ��ĥ�� �״� ��Ǻ��� �� �Ѿ�� �ͱ���
    }

    private IEnumerator WaitForDeath()
    {
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("Dead"))
        {
            yield return null; // Dead motion ��� �� ������ ���
        }

        // Dead motion ������ ���
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }

        // �״� ��� �� ����� �Ѿ�� �� 1�� ���
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("LoseScene");
        ResetHealth(); // ���� ���� �� ü���� �ʱ�ȭ
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
