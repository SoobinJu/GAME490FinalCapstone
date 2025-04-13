using UnityEngine;
using System.Collections;
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
        StartCoroutine(LoadGameCoroutine());
    }

    private IEnumerator LoadGameCoroutine()
    {
        // 2초 대기
        yield return new WaitForSeconds(0.5f);

        // 저장된 씬 이름 가져오기
        if (PlayerPrefs.HasKey("SavedScene"))
        {
            string savedScene = PlayerPrefs.GetString("SavedScene");
            Debug.Log("Loading Saved Scene after delay: " + savedScene);
            SceneManager.LoadScene(savedScene);
        }
        else
        {
            Debug.Log("No saved game found. Starting from Narration1 after delay");
            ResetGame();
            SceneManager.LoadScene("Narration1");
        }
    }

    public void ResetGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("All game data has been deleted.");
    }
}
