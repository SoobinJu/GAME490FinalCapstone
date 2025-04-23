using TMPro;
using UnityEngine;

public class ClueTrackerUI : MonoBehaviour
{
    public TextMeshProUGUI clueText;

    void Update()
    {
        int currentClues = GameProgressTracker.Instance.GetCluesFound();
        int totalClues = 9; // match your clue total
        clueText.text = currentClues + "/" + totalClues;
    }
}
