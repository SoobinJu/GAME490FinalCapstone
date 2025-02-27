using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;


public class SuspectHandler : MonoBehaviour
{
    public TextMeshProUGUI chancesText; // UI text for chances
    public string correctSuspect = "Suspector B"; // ✅ Correct suspect is now B!

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
        // List to store UI objects hit by raycast
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.CompareTag("Suspect")) // Make sure suspects have the "Suspect" tag
            {
                Debug.Log("Clicked on: " + result.gameObject.name);
                ChooseSuspect(result.gameObject.name);
                return;
            }
        }

        Debug.Log("❌ No suspect detected! UI blocking.");
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
