using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
    // 따로 만든 Canvas 안의 메시지 패널 또는 텍스트 오브젝트
    public GameObject saveMessageCanvas;

    void Start()
    {
        // 시작 시 메시지 Canvas 꺼두기
        if (saveMessageCanvas != null)
            saveMessageCanvas.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1)) // F1 = 초기화
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

        // 저장 메시지 표시
        if (saveMessageCanvas != null)
            StartCoroutine(ShowSaveMessage());
    }

    private IEnumerator ShowSaveMessage()
    {
        saveMessageCanvas.SetActive(true);
        yield return new WaitForSeconds(2f); // 2초 보여주기
        saveMessageCanvas.SetActive(false);
    }

    public void LoadGame()
    {
        StartCoroutine(LoadGameCoroutine());
    }

    private IEnumerator LoadGameCoroutine()
    {
        yield return new WaitForSeconds(0.5f);

        if (PlayerPrefs.HasKey("SavedScene"))
        {
            string savedScene = PlayerPrefs.GetString("SavedScene");
            Debug.Log("Loading Saved Scene after delay: " + savedScene);
            SceneManager.LoadScene(savedScene);
        }
        else
        {
            Debug.Log("No saved game found. Starting from Game1 after delay");
            ResetGame();
            SceneManager.LoadScene("Game1");
        }
    }

    public void ResetGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("All game data has been deleted.");
    }
}
