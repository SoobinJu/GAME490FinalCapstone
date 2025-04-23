using UnityEngine;

public class GonjangHP : MonoBehaviour
{
    void Start()
    {
        // Check the saved minigame result
        int result = PlayerPrefs.GetInt("GongjangResult", -1);

        if (result == 1) // 1 means they won!
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
            {
                PlayerHealth healthScript = player.GetComponent<PlayerHealth>();
                if (healthScript != null)
                {
                    float currentHP = healthScript.GetHealth();
                    float boostedHP = Mathf.Min(currentHP + 50f, healthScript.maxHealth); // don't go over max

                    healthScript.SetHealth(boostedHP);
                    Debug.Log("🎉 +50 HP awarded! New HP: " + boostedHP);
                }
                else
                {
                    Debug.LogError("❌ PlayerHealth component not found!");
                }
            }
            else
            {
                Debug.LogError("❌ Player not found in the scene!");
            }
        }

        // Clear the result so this only happens once
        PlayerPrefs.SetInt("GongjangResult", -1);
    }
}
