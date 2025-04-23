using UnityEngine;

public class GameProgressTracker : MonoBehaviour
{
    public static GameProgressTracker Instance;

    private int cluesFound = 0;
    private int totalClues = 9;  // Adjust this to match your actual number of clues
    private int quizzesSolved = 0;
    private int totalQuizzes = 3; // Adjust this to match your actual number of quizzes

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void FoundClue()
    {
        cluesFound++;
        Debug.Log("Clue Found! Progress: " + cluesFound + "/" + totalClues);
    }

    public void SolvedQuiz()
    {
        quizzesSolved++;
        Debug.Log("Quiz Solved! Progress: " + quizzesSolved + "/" + totalQuizzes);
    }

    public bool CanEnterGanak()
    {
        return cluesFound >= totalClues && quizzesSolved >= totalQuizzes;
    }

    public void ResetProgress()
    {
        cluesFound = 0;
        quizzesSolved = 0;
        Debug.Log("🧹 Progress reset! Current scene: " + UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }


    public int GetCluesFound() => cluesFound;
    public int GetQuizzesSolved() => quizzesSolved;

    public void SetCluesFound(int value) => cluesFound = value;
    public void SetQuizzesSolved(int value) => quizzesSolved = value;

}
