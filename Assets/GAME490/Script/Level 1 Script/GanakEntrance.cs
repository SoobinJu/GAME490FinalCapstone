using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GanakEntrance : MonoBehaviour
{
    public GameObject warningPanel;  // UI panel to show if entry is locked
    public GameObject enterButton;  // Enter button UI
    public int ganakSceneIndex;  // Scene index for Ganak

    private bool isPlayerNear = false;
    private bool isWPanelOpen = false;

    private void Start()
    {
        if (warningPanel != null) warningPanel.SetActive(false);  // Hide warning panel at start
        if (enterButton != null) enterButton.SetActive(false);  // Hide enter button at start
    }

    private void Update()
    {
        if (isPlayerNear && (Input.GetKeyDown(KeyCode.E)))
        {
            if (isWPanelOpen)
            {
                ClosePanel();
            }
            else
            {
                TryEnterGanak();
            }
        }
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
            ClosePanel();
        }
    }

    public void TryEnterGanak()
    {
            GameProgressTracker tracker = FindObjectOfType<GameProgressTracker>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (tracker != null && tracker.CanEnterGanak())
            {
            // Save the player's current position BEFORE entering the building
            PlayerPrefs.SetFloat("LastExitX", player.transform.position.x);
            PlayerPrefs.SetFloat("LastExitY", player.transform.position.y);
            PlayerPrefs.SetInt("ReturningFromBuilding", 1); // Mark that we are returning
            PlayerPrefs.Save();
            Debug.Log("Saved Player Entrance Position BEFORE entering building: X=" + player.transform.position.x + " Y=" + player.transform.position.y);

            Debug.Log("Entering Ganak...");
                SceneManager.LoadScene(ganakSceneIndex);
            }
            else
            {
                Debug.Log("Can't enter Ganak yet. Missing clues or quizzes.");
                if (warningPanel != null)
                {
                    warningPanel.SetActive(true);  // Show warning message
                    isWPanelOpen = true;
                }
            }
    }

    public void ClosePanel()
    {
        warningPanel.SetActive(false);
        isWPanelOpen = false;
    }
}
