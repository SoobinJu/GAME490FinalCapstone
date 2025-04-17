using UnityEngine;

public class MinigameResultChecker : MonoBehaviour
{
    public PlayerHealth playerHealth;

    void Start()
    {
        int result = PlayerPrefs.GetInt("GongjangResult", -1);
        if (result == 1)
        {
            playerHealth.SetHealth(playerHealth.GetHealth() + 50f);
        }
        PlayerPrefs.SetInt("GongjangResult", -1);
    }
}
