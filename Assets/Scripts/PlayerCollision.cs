using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public Animator animator;
    public HealthManager healthManager;
    public float damageAmount = 10f;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Obstacle")
        {
            healthManager.TakeDamage(damageAmount);
            animator.SetBool("IsDamaged", true);
        }
        else
        {
            animator.SetBool("IsDamaged", false);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Collectible"))
        {
            // �ݷ��ͺ� ȹ��
            collectibleSpawner spawner = FindObjectOfType<collectibleSpawner>(); // Spawner ã��
            spawner.Collect(other.gameObject); // Collect �޼��� ȣ��
        }
    }

}
