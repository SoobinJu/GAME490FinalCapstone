using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField]
    private Slider hpbar;

    private float maxHp = 100;
    private float curHp = 100;

    void Start()
    {
        hpbar.value = curHp / maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        // Update UI based on current HP
        hpbar.value = curHp / maxHp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Collided with: {collision.gameObject.name}");

        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Collided with Enemy!");
            TakeDamage(10); // Reduce HP by 10
        }
    }

    private void TakeDamage(float damage)
    {
        curHp -= damage;
        curHp = Mathf.Clamp(curHp, 0, maxHp); // Prevent HP from going below 0 or above maxHp

        // Optionally, check if the player is dead
        if (curHp <= 0)
        {
            Debug.Log("Player is dead!");
            // Add death logic here
        }
    }
}
