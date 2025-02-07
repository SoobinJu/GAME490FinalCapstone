using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class SuspectHandler : MonoBehaviour
{
    public TextMeshProUGUI chancesText; // UI text for chances
    public string correctSuspect = "A"; // The correct suspect

    private int chances; // Total chances

    private void Start()
    {
        // 🛑 Always load chances from PlayerPrefs (default to 3 if no value exists)
        chances = PlayerPrefs.GetInt("Chances", 3);
        UpdateChancesText();
    }

    private void Update()
    {
        // Detect mouse click
        if (Input.GetMouseButtonDown(0))
        {
            DetectSuspectClick();
        }
    }

    private void DetectSuspectClick()
    {
        // If the mouse click is over a UI element, do nothing
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return; // Ignore clicks on UI elements
        }

        // Cast a ray from the camera to the world point of the mouse position
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null)
        {
            // Check if the clicked object is a suspect
            string clickedSuspectName = hit.collider.gameObject.name; // Get the suspect's GameObject name
            ChooseSuspect(clickedSuspectName); // Pass the name to the ChooseSuspect method
        }
    }

    public void ChooseSuspect(string suspectName)
    {
        if (suspectName == correctSuspect)
        {
            // ✅ Save chances before switching scenes
            PlayerPrefs.SetInt("Chances", chances);
            PlayerPrefs.Save();

            Debug.Log("✅ Correct! Moving to next level...");
            SceneManager.LoadScene("Narration2"); // Go to the next scene
        }
        else
        {
            StartDecreaseChances();
        }
    }

    public void StartDecreaseChances()
    {
        StartCoroutine(DecreaseChances());
    }

    private IEnumerator DecreaseChances()
    {
        // ❌ When player chooses the wrong suspect, decrease chances
        chances--;
        PlayerPrefs.SetInt("Chances", chances); // 🔹 Save updated chances
        PlayerPrefs.Save();
        UpdateChancesText();
        Debug.Log("❌ Wrong! Chances left: " + chances);

        if (chances <= 0)
        {
            Debug.Log("💀 Game Over! No chances left.");
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene("LoseScene"); // Go to the Lose scene
        }
    }

    private void UpdateChancesText()
    {
        // 🔹 Update UI text to show remaining chances
        chancesText.text = chances + "/3";

        if (chances <= 0)
        {
            chancesText.color = Color.red; // Change text color to red when losing
        }
        else
        {
            chancesText.color = Color.white; // Keep it white normally
        }
    }
}
