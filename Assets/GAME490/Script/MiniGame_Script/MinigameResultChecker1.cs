using UnityEngine;

public class MinigameResultChecker1 : MonoBehaviour
{
    public PlayerHealth playerHealth;

    void Start()
    {
        int result = PlayerPrefs.GetInt("RicecakeResult", -1);
        if (result == 1)
        {
            playerHealth.SetHealth(playerHealth.GetHealth() + 50f);
        }
        PlayerPrefs.SetInt("RicecakeResult", -1);
    }
}
