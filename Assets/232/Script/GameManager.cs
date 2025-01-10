using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // 게임 상태 저장 (레벨, 플레이어 상태 등)
    public void SaveGame(int currentLevel)
    {
        PlayerPrefs.SetInt("CurrentLevel", currentLevel); // 현재 레벨 저장
        PlayerPrefs.Save(); // 저장
        Debug.Log("게임 저장됨, 레벨: " + currentLevel);
    }

    // 저장된 게임 상태 불러오기
    public int LoadGame()
    {
        int savedLevel = PlayerPrefs.GetInt("CurrentLevel", 1); // 기본값 1
        return savedLevel;
    }

    // 게임 종료 처리
    public void ExitGame()
    {
        PlayerPrefs.Save(); // 상태 저장
        Application.Quit(); // 게임 종료
    }

    // 게임 시작 시 저장된 레벨 불러오기
    public void StartGame()
    {
        int savedLevel = LoadGame(); // 저장된 레벨을 불러옴
        LoadLevel(savedLevel); // 해당 레벨로 로드
    }

    // 특정 레벨로 씬 전환
    private void LoadLevel(int level)
    {
        SceneManager.LoadScene(level); // 저장된 레벨로 씬 로드
    }
}
