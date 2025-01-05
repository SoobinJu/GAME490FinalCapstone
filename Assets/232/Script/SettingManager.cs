using UnityEngine;

public class SettingManager : MonoBehaviour
{
    public GameObject settingsPanel;  // 설정 패널을 연결할 변수
    private bool isGamePaused = false;  // 게임이 일시 정지 상태인지 확인하는 변수

    void Update()
    {
        // ESC 키를 눌렀을 때
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                ResumeGame();  // 게임 재개
            }
            else
            {
                PauseGame();  // 게임 일시 정지
            }
        }
    }

    // 게임 일시 정지
    void PauseGame()
    {
        settingsPanel.SetActive(true);  // 설정 패널 활성화
        Time.timeScale = 0f;  // 게임 일시 정지
        isGamePaused = true;  // 게임이 일시 정지 상태로 변경
    }

    // 게임 재개
    void ResumeGame()
    {
        settingsPanel.SetActive(false);  // 설정 패널 비활성화
        Time.timeScale = 1f;  // 게임 진행
        isGamePaused = false;  // 게임 재개 상태로 변경
    }
}
