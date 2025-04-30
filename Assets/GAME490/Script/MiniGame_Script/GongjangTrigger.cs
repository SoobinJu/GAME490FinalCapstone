using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GonjangTrigger : MonoBehaviour
{
    public string triggerID = "Minigame_Punish1"; // 👈 unique PlayerPrefs key for one-time check
    public GameObject messageTextObject; // 👈 Drag your TextMeshProUGUI message object
    private TextMeshProUGUI messageText;

    private bool triggered = false;

    void Start()
    {
        if (messageTextObject != null)
        {
            messageText = messageTextObject.GetComponent<TextMeshProUGUI>();
            messageTextObject.SetActive(false);
        }

        // 🛑 If this minigame has already been played
        if (PlayerPrefs.GetInt(triggerID, 0) == 1)
        {
            triggered = true;
            gameObject.SetActive(false); // Disable this trigger permanently
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered || !other.CompareTag("Player")) return;

        triggered = true;
        PlayerPrefs.SetInt(triggerID, 1); // ✅ Save that it's been used
        PlayerPrefs.Save();

        // Save return position
        PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetFloat("MinigameReturnX", other.transform.position.x);
        PlayerPrefs.SetFloat("MinigameReturnY", other.transform.position.y);

        // Show UI message
        if (messageTextObject != null)
        {
            messageTextObject.SetActive(true);
            messageText.text = "Entering punishment minigame...";
        }

        Invoke("GoToMinigame", 2f); // ✨ Add delay
    }

    void GoToMinigame()
    {
        SceneManager.LoadScene("Minigame"); // Change to your real scene name if needed
    }
}
