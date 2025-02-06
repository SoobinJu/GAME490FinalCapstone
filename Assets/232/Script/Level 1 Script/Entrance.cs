using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Entrance : MonoBehaviour
{
    public int sceneBuildIndex; // Scene to load
    public GameObject enterButton; // UI Button
    private bool isPlayerNear = false; // Check if player is near

    private void Start()
    {
        enterButton.SetActive(false); // Hide the button at start
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            print("Player is near the entrance");
            isPlayerNear = true;
            enterButton.SetActive(true); // Show button
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            print("Player left the entrance area");
            isPlayerNear = false;
            enterButton.SetActive(false); // Hide button
        }
    }

    public void EnterScene()
    {
        if (isPlayerNear) // Ensure player is near when clicking
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
            {
                // Save the player's position and the entrance ID
                PlayerPrefs.SetFloat("LastEntranceX", player.transform.position.x);
                PlayerPrefs.SetFloat("LastEntranceY", player.transform.position.y);
                PlayerPrefs.SetString("LastBuilding", SceneManager.GetActiveScene().name); // Store which scene they entered from
                PlayerPrefs.SetInt("Returning", 1); // Mark that we are returning
                PlayerPrefs.Save(); // Save data
            }

            print("Switching Scene to " + sceneBuildIndex);
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
        }
    }
}
