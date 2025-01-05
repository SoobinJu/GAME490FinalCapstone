using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Animator animator;
    public Image healthBar; // UI Image for the health bar
    public float healthAmount = 100f; // Starting health
    public GameObject gameOverPanel; // Reference to the Game Over panel

    void Start()
    {
        // Ensure GameOverPanel is initially inactive
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    void Update()
    {
        // Check if health is 0 or less
        if (healthAmount <= 0)
        {
            animator.SetBool("IsDead", true);
            GameOver();
        }
        else
        {
            animator.SetBool("IsDead", false);
        }
    }

    public void TakeDamage(float damage)
    {
        healthAmount -= damage;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100); // Prevent negative health
        healthBar.fillAmount = healthAmount / 100f;
    }

    public void Heal(float healingAmount)
    {
        healthAmount += healingAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100); // Prevent exceeding max health
        healthBar.fillAmount = healthAmount / 100f;
    }

    private void GameOver()
    {
        // Activate the Game Over panel
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
        

    }
    
}
