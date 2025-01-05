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
            // 콜렉터블 획득
            collectibleSpawner spawner = FindObjectOfType<collectibleSpawner>(); // Spawner 찾기
            spawner.Collect(other.gameObject); // Collect 메서드 호출
        }
    }

}
