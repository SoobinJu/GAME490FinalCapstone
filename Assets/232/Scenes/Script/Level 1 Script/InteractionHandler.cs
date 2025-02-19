using Unity.VisualScripting;
using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    public GameObject panel; // Panel that shows up
    private bool canOpen = false;
    private bool panelOpen = false;
    private bool clueFounded = false;

    private void Start()
    {
        // Hide everything at the start
        panel.SetActive(false);
    }

    private void Update()
    {
        if (canOpen && (Input.GetKeyDown(KeyCode.E)))
        {
            if (panelOpen)
            {
                ClosePanel(); // E Ű�� �г� �ݱ�
            }
            else
            {
                OpenPanel(); // E Ű�� �г� ����
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // When the player comes near
        {
            canOpen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // When the player goes away
        {
            canOpen = false;
            ClosePanel();
        }
    }

    public void OpenPanel()
    {
        panel.SetActive(true); // Show the panel
        panelOpen = true;

        if (!clueFounded)
        {
            clueFounded = true;
            GameProgressTracker tracker = FindObjectOfType<GameProgressTracker>();

            if (tracker != null)
            {
                tracker.FoundClue(); // Register clue progress
                Debug.Log("Clue Found! Progress updated.");
            }
            else
            {
                Debug.LogError("GameProgressTracker not found! Trying to create one...");
                GameObject newTracker = new GameObject("GameProgressTracker");
                tracker = newTracker.AddComponent<GameProgressTracker>(); // Create a new tracker
                tracker.FoundClue();
            }
        }
    }

    public void ClosePanel()
    {
        panel.SetActive(false); // Hide the panel
        panelOpen = false;
    }
}

