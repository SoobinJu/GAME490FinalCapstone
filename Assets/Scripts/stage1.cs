using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class stage1 : MonoBehaviour
{
    public GameObject SettingsPanel;

    public void Main()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
        UnpauseGame(); // Ensure the game is not paused in MainMenu
    }

    public void howtoplay()
    {
        SceneManager.LoadScene("howtoplay");
        UnpauseGame();
    }

    public void Restart()
    {
        Debug.Log("Restart Button Clicked!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        UnpauseGame();
    }





    public void SetActive()
    {
        if (SettingsPanel.activeSelf)
        {
            SettingsPanel.SetActive(false);
            UnpauseGame();
        }
        else
        {
            SettingsPanel.SetActive(true);
            PauseGame();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
    }

    private void UnpauseGame()
    {
        Time.timeScale = 1;
    }
}
