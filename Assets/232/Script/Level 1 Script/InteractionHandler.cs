using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    public GameObject openButton; // Button that appears near the object
    public GameObject closeButton; // Button inside the panel
    public GameObject panel; // Panel that shows up

    private void Start()
    {
        // Hide everything at the start
        openButton.SetActive(false);
        closeButton.SetActive(false);
        panel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // When the player comes near
        {
            openButton.SetActive(true); // Show the open button
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // When the player goes away
        {
            openButton.SetActive(false); // Hide the open button
        }
    }

    public void OnOpenButtonClick()
    {
        openButton.SetActive(false); // Hide the open button
        panel.SetActive(true); // Show the panel
        closeButton.SetActive(true); // Show the close button
    }

    public void OnCloseButtonClick()
    {
        panel.SetActive(false); // Hide the panel
        closeButton.SetActive(false); // Hide the close button
        openButton.SetActive(true); // Show the open button again
    }
}

