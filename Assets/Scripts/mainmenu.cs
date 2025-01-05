using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("CutScene");
        UnpauseGame();
    }

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
        Time.timeScale = 1;
        Debug.Log("Restart Button Clicked!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        UnpauseGame();
    }

    // Skip button
    public void SkipDialogue()
    {
        StopAllCoroutines();
        SceneManager.LoadScene("stage1");
        UnpauseGame(); // Ensure gameplay starts correctly
    }

    public void Quit()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }

    public GameObject SettingsPanel;

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
