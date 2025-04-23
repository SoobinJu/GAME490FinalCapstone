using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameTrigger : MonoBehaviour
{
    public GameObject player;
    public PlayerHealth playerHealth;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) // M키로 테스트
        {
            SaveGameState();
            SceneManager.LoadScene("Minigame");
        }
    }

    void SaveGameState()
    {
        GameState.Instance.savedPlayerPosition = player.transform.position;
        GameState.Instance.savedPlayerHealth = playerHealth.GetHealth();
        GameState.Instance.savedCluesFound = GameProgressTracker.Instance.GetCluesFound();
        GameState.Instance.savedQuizzesSolved = GameProgressTracker.Instance.GetQuizzesSolved();
        GameState.Instance.savedChances = PlayerPrefs.GetInt("Chances", 3);

        PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();

        Debug.Log("🎮 상태 저장 완료!");
    }

}
