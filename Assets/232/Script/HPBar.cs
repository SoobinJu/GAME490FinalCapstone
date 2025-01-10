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

    void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

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
        // ü�� ����
        health -= damageAmount;
        health = Mathf.Clamp(health, 0, maxHealth); // ü�� ���� ����
        UpdateHealthbar();

        // ü�� 0�� �Ǹ� ó��
        if (health <= 0)
        {
            OnHealthDepleted();
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

    private void OnHealthDepleted()
    {
        // ü���� 0�� �Ǿ��� �� LoseScene���� �̵�
        ResetHealth(); // ���� ���� �� ü���� �ʱ�ȭ
        SceneManager.LoadScene("LoseScene");
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
