using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1)) // F1을 누르면 저장 초기화
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            Debug.Log("All data was deleted");
        }
    }

    public void SaveGame()
    {
        // 현재 씬 이름 저장
        string currentScene = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("SavedScene", currentScene);

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
            SceneManager.LoadScene("Narration1");
            Debug.Log("No saved game found. Starting from Narration1");
            ResetGame();
        }
    }

    public void ResetGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("All game data has been deleted.");
    }

}
