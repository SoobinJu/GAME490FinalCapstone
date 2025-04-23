using UnityEngine;
using System.Collections;

public class GameStateRestorer : MonoBehaviour
{
    public GameObject player;
    public PlayerHealth playerHealth;

    void Start()
    {
        StartCoroutine(RestoreAfterDelay());

        int savedChances = PlayerPrefs.GetInt("SavedChancesBeforeQuiz", 3);
        PlayerPrefs.SetInt("Chances", savedChances);
        PlayerPrefs.Save();

        Debug.Log("복원된 찬스: " + savedChances);

    }

    IEnumerator RestoreAfterDelay()
    {
        yield return new WaitForSeconds(0.1f);

        // 🎯 미니게임 복원 조건
        string prevScene = PlayerPrefs.GetString("PreviousScene", "");

        if (prevScene == "Minigame" && GameState.Instance != null)
        {
            player.transform.position = GameState.Instance.savedPlayerPosition;
            playerHealth.SetHealth(GameState.Instance.savedPlayerHealth);
            GameProgressTracker.Instance.SetCluesFound(GameState.Instance.savedCluesFound);
            GameProgressTracker.Instance.SetQuizzesSolved(GameState.Instance.savedQuizzesSolved);
            Debug.Log("✅ 미니게임 복원 완료");
        }

        // ✅ chances 복원은 항상 진행 (미니게임이든 건물이든)
        int saved = PlayerPrefs.GetInt("SavedChancesBeforeQuiz", 3);
        PlayerPrefs.SetInt("Chances", saved);
        PlayerPrefs.Save();
        Debug.Log("♻ Game1 씬에서 chances 복원됨: " + saved);
    }


}
