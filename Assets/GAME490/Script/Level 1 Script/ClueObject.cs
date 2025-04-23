using UnityEngine;

public class ClueObject : MonoBehaviour
{
    private bool alreadyCollected = false;
    private bool playerInRange = false;

    public GameObject cluePanel; // assign this in inspector

    void Update()
    {
        if (playerInRange && !alreadyCollected && Input.GetKeyDown(KeyCode.E))
        {
            ShowClue();
        }
    }

    private void ShowClue()
    {
        cluePanel.SetActive(true); // open clue UI
        GameProgressTracker.Instance.FoundClue(); // count clue
        alreadyCollected = true; // make sure it only works once

        Debug.Log("📜 Clue collected: " + gameObject.name);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
