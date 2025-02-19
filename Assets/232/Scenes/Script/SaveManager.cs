using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public void SaveGame()
    {
        // 현재 씬 이름 저장
        string currentScene = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("SavedScene", currentScene);

        // 필요한 추가 데이터 저장
        // 예: PlayerPrefs.SetInt("Score", currentScore);

        PlayerPrefs.Save();
        Debug.Log("Game Saved: " + currentScene);
    }

    public void LoadGame()
    {
        // 저장된 씬 이름 가져오기
        if (PlayerPrefs.HasKey("SavedScene"))
        {
            string savedScene = PlayerPrefs.GetString("SavedScene");
            SceneManager.LoadScene(savedScene); // 저장된 씬으로 이동
            Debug.Log("Loading Saved Scene: " + savedScene);
        }
        else
        {
            // 저장된 데이터가 없으면 기본 첫 번째 레벨로 이동
            SceneManager.LoadScene("Game1");
            Debug.Log("No saved game found. Starting from Game1.");
        }
    }

}
