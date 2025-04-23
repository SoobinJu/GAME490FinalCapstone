using UnityEngine;

public class RiceCakeHP : MonoBehaviour
{
    void Start()
    {
        int result = PlayerPrefs.GetInt("RicecakeResult", -1);
        if (result == 1)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
            {
                PlayerHealth healthScript = player.GetComponent<PlayerHealth>();
                if (healthScript != null)
                {
                    float currentHP = healthScript.GetHealth();
                    float boostedHP = Mathf.Min(currentHP + 50f, healthScript.maxHealth); // prevent going above max

                    healthScript.SetHealth(boostedHP);
                    Debug.Log("🎉 HP +50 from Minigame! New HP: " + boostedHP);
                }
            }
        }

        PlayerPrefs.SetInt("RicecakeResult", -1); // prevent re-applying boost again
    }
}
