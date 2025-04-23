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

    private void Update()
    {
        if (isPlayerNear && (Input.GetKeyDown(KeyCode.E)))
        {
            EnterScene();
        }
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
        if (isPlayerNear)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
            {
                string currentScene = SceneManager.GetActiveScene().name;
                string targetScene = SceneManager.GetSceneByBuildIndex(sceneBuildIndex).name;

                // ✅ Game1 → 건물 진입할 때 (위치 + chances 저장)
                if (currentScene == "Game1")
                {
                    PlayerPrefs.SetFloat("LastExitX", player.transform.position.x);
                    PlayerPrefs.SetFloat("LastExitY", player.transform.position.y);
                    PlayerPrefs.SetInt("ReturningFromBuilding", 1);

                    int chances = PlayerPrefs.GetInt("Chances", 3);
                    PlayerPrefs.SetInt("SavedChancesBeforeQuiz", chances);
                    PlayerPrefs.Save();

                    Debug.Log($"➡️ Game1 → {targetScene} | 위치 & chances 저장 완료: Pos={player.transform.position}, Chances={chances}");
                }

                // ✅ 건물 → Game1 나갈 때 (chances만 저장)
                else if (targetScene == "Game1")
                {
                    int currentChances = PlayerPrefs.GetInt("Chances", 3);
                    PlayerPrefs.SetInt("SavedChancesBeforeQuiz", currentChances);
                    PlayerPrefs.Save();

                    Debug.Log($"🏠 건물 → Game1 | 줄어든 chances 저장 완료: {currentChances}");
                }
            }

            SceneManager.LoadScene(sceneBuildIndex);
        }
    }



}



