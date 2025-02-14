using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GanakEntrance : MonoBehaviour
{
    public GameObject warningPanel;  // UI panel to show if entry is locked
    public GameObject enterButton;  // Enter button UI
    public int ganakSceneIndex;  // Scene index for Ganak

    private bool isPlayerNear = false;

    private void Start()
    {
        if (warningPanel != null) warningPanel.SetActive(false);  // Hide warning panel at start
        if (enterButton != null) enterButton.SetActive(false);  // Hide enter button at start
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            if (enterButton != null) enterButton.SetActive(true);  // Show enter button
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            if (enterButton != null) enterButton.SetActive(false);  // Hide enter button
            if (warningPanel != null) warningPanel.SetActive(false);  // Hide warning panel
        }
    }

    public void TryEnterGanak()
    {
        if (isPlayerNear)
        {
            GameProgressTracker tracker = FindObjectOfType<GameProgressTracker>();

            if (tracker != null && tracker.CanEnterGanak())
            {
                Debug.Log("✅ Entering Ganak...");
                SceneManager.LoadScene(ganakSceneIndex);
            }
            else
            {
                Debug.Log("❌ Can't enter Ganak yet. Missing clues or quizzes.");
                if (warningPanel != null) warningPanel.SetActive(true);  // Show warning message
            }
        }
    }
}
