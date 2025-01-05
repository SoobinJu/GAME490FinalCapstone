using UnityEngine;

public class SettingManager : MonoBehaviour
{
    public GameObject settingsPanel;  // ���� �г��� ������ ����
    private bool isGamePaused = false;  // ������ �Ͻ� ���� �������� Ȯ���ϴ� ����

    void Update()
    {
        // ESC Ű�� ������ ��
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                ResumeGame();  // ���� �簳
            }
            else
            {
                PauseGame();  // ���� �Ͻ� ����
            }
        }
    }

    // ���� �Ͻ� ����
    void PauseGame()
    {
        settingsPanel.SetActive(true);  // ���� �г� Ȱ��ȭ
        Time.timeScale = 0f;  // ���� �Ͻ� ����
        isGamePaused = true;  // ������ �Ͻ� ���� ���·� ����
    }

    // ���� �簳
    void ResumeGame()
    {
        settingsPanel.SetActive(false);  // ���� �г� ��Ȱ��ȭ
        Time.timeScale = 1f;  // ���� ����
        isGamePaused = false;  // ���� �簳 ���·� ����
    }
}
