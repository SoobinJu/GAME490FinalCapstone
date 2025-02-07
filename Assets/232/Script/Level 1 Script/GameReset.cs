using UnityEngine;
using UnityEngine.SceneManagement;

public class GameReset : MonoBehaviour
{
    public void ResetGame()
    {
        PlayerPrefs.SetInt("PlayerChances", 3); // Reset chances to 3
        PlayerPrefs.Save();
        SceneManager.LoadScene("OriginalScene"); // Load the starting scene (replace with your actual scene name)
    }
}
