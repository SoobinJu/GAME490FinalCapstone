using Unity.VisualScripting;
using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    public GameObject panel; // Panel that shows up
    private bool canOpen = false;
    private bool panelOpen = false;
    private bool clueFounded = false;

    private SpriteRenderer spriteRenderer;
    public Sprite defaultImage;
    public Sprite changedImage;

    private void Start()
    {
        // Hide everything at the start
        panel.SetActive(false);

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = defaultImage;
    }

    private void Update()
    {
        if (canOpen && (Input.GetKeyDown(KeyCode.E)))
        {
            if (panelOpen)
            {
                ClosePanel(); // E 키로 패널 닫기
            }
            else
            {
                OpenPanel(); // E 키로 패널 열기
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // When the player comes near
        {
            canOpen = true;
            spriteRenderer.sprite = changedImage;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // When the player goes away
        {
            canOpen = false;
            ClosePanel();
            spriteRenderer.sprite = defaultImage;
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

