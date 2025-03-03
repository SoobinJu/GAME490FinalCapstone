using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public void SaveGame()
    {
        // ���� �� �̸� ����
        string currentScene = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("SavedScene", currentScene);

        PlayerPrefs.Save();
        Debug.Log("Game Saved: " + currentScene);
    }

    public void LoadGame()
    {
        // ����� �� �̸� ��������
        if (PlayerPrefs.HasKey("SavedScene"))
        {
            string savedScene = PlayerPrefs.GetString("SavedScene");
            SceneManager.LoadScene(savedScene); // ����� ������ �̵�
            Debug.Log("Loading Saved Scene: " + savedScene);
        }
        else
        {
            // ����� �����Ͱ� ������ �⺻ ù ��° ������ �̵�
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
