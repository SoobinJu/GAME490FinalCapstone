using UnityEngine;

public class MinigameReturnHandler : MonoBehaviour
{
    void Start()
    {
        // 👀 Only load position if returning from Minigame
        if (PlayerPrefs.HasKey("MinigameReturnX") && PlayerPrefs.HasKey("MinigameReturnY"))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                float x = PlayerPrefs.GetFloat("MinigameReturnX");
                float y = PlayerPrefs.GetFloat("MinigameReturnY");
                player.transform.position = new Vector2(x, y);

                Debug.Log("✅ Returned to saved position: " + x + ", " + y);

                // Clear after use
                PlayerPrefs.DeleteKey("MinigameReturnX");
                PlayerPrefs.DeleteKey("MinigameReturnY");
            }
        }
    }
}
