using UnityEngine;
using UnityEngine.UI;

public class QuizButton : MonoBehaviour
{
    private Button quizButton;

    private void Start()
    {
        quizButton = GetComponent<Button>();
        quizButton.onClick.AddListener(OnCorrectAnswer);
    }

    private void OnCorrectAnswer()
    {
        // Try to find GameProgressTracker
        GameProgressTracker tracker = FindObjectOfType<GameProgressTracker>();

        if (tracker != null)
        {
            tracker.SolvedQuiz(); // ✅ Register quiz progress
            Debug.Log("✅ Quiz Solved! Progress updated.");
        }
        else
        {
            Debug.LogError("❌ GameProgressTracker not found! Trying to create one...");
            GameObject newTracker = new GameObject("GameProgressTracker");
            tracker = newTracker.AddComponent<GameProgressTracker>(); // Create a new tracker
            tracker.SolvedQuiz();
        }
    }
}
