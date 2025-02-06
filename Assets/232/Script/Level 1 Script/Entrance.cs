using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // For UI elements

public class Entrance : MonoBehaviour
{
    public int sceneBuildIndex; // The index of the scene to load
    public GameObject enterButton; // Reference to the UI button

    private bool isPlayerNear = false; // Track if the player is near the entrance

    private void Start()
    {
        enterButton.SetActive(false); // Hide the button at the start
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            print("Player is near the entrance");
            isPlayerNear = true;
            enterButton.SetActive(true); // Show the button
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            print("Player left the entrance area");
            isPlayerNear = false;
            enterButton.SetActive(false); // Hide the button
        }
    }

    public void EnterScene()
    {
        if (isPlayerNear) // Ensure the player is still near when clicking
        {
            print("Switching Scene to " + sceneBuildIndex);
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
        }
    }
}
