using UnityEngine;

public class SettingManager : MonoBehaviour
{
    public GameObject settingsPanel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            if (settingsPanel.activeSelf)
            {
                settingsPanel.SetActive(false);
                UpdateGamePauseState();
            }
            else
            {
                settingsPanel.SetActive(true);
                UpdateGamePauseState();
            }
        }
    }

    private void UpdateGamePauseState()
    {
        if (settingsPanel.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}
