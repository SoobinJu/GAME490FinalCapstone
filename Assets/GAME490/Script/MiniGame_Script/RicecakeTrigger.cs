using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class RicecakeTrigger : MonoBehaviour
{
    public string triggerID = "Minigame_Tteok1";
    private bool triggered = false;

    public GameObject messageTextObject; // 👈 drag MiniGameMessage here in Inspector
    private TextMeshProUGUI messageText;

    void Start()
    {
        if (PlayerPrefs.GetInt(triggerID, 0) == 1)
        {
            triggered = true;
            gameObject.SetActive(false);
        }

        if (messageTextObject != null)
        {
            messageText = messageTextObject.GetComponent<TextMeshProUGUI>();
            messageTextObject.SetActive(false); // Hide by default
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered || !other.CompareTag("Player")) return;

        triggered = true;
        PlayerPrefs.SetInt(triggerID, 1);
        PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetFloat("MinigameReturnX", other.transform.position.x);
        PlayerPrefs.SetFloat("MinigameReturnY", other.transform.position.y);

        if (messageTextObject != null)
        {
            messageTextObject.SetActive(true);
            messageText.text = "Entering quick minigame...";
        }

        Invoke("GoToMinigame", 2f); // Wait 2 seconds before changing scene
    }

    void GoToMinigame()
    {
        SceneManager.LoadScene("Minigame2");
    }
}
