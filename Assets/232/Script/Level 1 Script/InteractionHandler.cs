using Unity.VisualScripting;
using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    public GameObject openButton; // Button that appears near the object
    public GameObject panel; // Panel that shows up
    private bool canOpen = false;
    private bool panelOpen = false;

    private void Start()
    {
        // Hide everything at the start
        openButton.SetActive(false);
        panel.SetActive(false);
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
            openButton.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // When the player goes away
        {
            canOpen = false;
            openButton.SetActive(false);
            ClosePanel();
        }
    }

    public void OpenPanel()
    {
        openButton.SetActive(false); // Hide the open button
        panel.SetActive(true); // Show the panel
        panelOpen = true;
    }

    public void ClosePanel()
    {
        panel.SetActive(false); // Hide the panel
        openButton.SetActive(true); // Show the open button again
        panelOpen = false;
    }
}

