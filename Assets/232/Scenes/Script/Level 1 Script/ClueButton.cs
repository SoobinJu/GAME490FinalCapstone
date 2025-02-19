using UnityEngine;
using UnityEngine.UI;

public class ClueButton : MonoBehaviour
{
    private Button clueButton;

    private void Start()
    {
        clueButton = GetComponent<Button>();
        clueButton.onClick.AddListener(OnClueFound);
    }

    private void OnClueFound()
    {
        GameProgressTracker tracker = FindObjectOfType<GameProgressTracker>();

        if (tracker != null)
        {
            tracker.FoundClue(); // ✅ Register clue progress
            Debug.Log("🔍 Clue Found! Progress updated.");
        }
        else
        {
            Debug.LogError("❌ GameProgressTracker not found! Trying to create one...");
            GameObject newTracker = new GameObject("GameProgressTracker");
            tracker = newTracker.AddComponent<GameProgressTracker>(); // Create a new tracker
            tracker.FoundClue();
        }
    }
}
