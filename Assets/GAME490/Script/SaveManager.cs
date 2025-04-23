using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
    // ���� ���� Canvas ���� �޽��� �г� �Ǵ� �ؽ�Ʈ ������Ʈ
    public GameObject saveMessageCanvas;

    void Start()
    {
        // ���� �� �޽��� Canvas ���α�
        if (saveMessageCanvas != null)
            saveMessageCanvas.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1)) // F1 = �ʱ�ȭ
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            Debug.Log("All data was deleted");
        }
    }

    public void SaveGame()
    {
        // ���� �� �̸� ����
        string currentScene = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("SavedScene", currentScene);
        PlayerPrefs.Save();
        Debug.Log("Game Saved: " + currentScene);

        // ���� �޽��� ǥ��
        if (saveMessageCanvas != null)
            StartCoroutine(ShowSaveMessage());
    }

    private IEnumerator ShowSaveMessage()
    {
        saveMessageCanvas.SetActive(true);
        yield return new WaitForSeconds(2f); // 2�� �����ֱ�
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
