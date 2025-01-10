using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameManager gameManager; // GameManager를 연결

    // 레벨 종료 시 호출될 함수 (나레이션 화면에서 버튼을 표시)
    public void OnLevelEnd()
    {
        // Save 버튼과 Next 버튼을 화면에 표시하는 UI를 활성화
        // 예를 들어, saveButton.SetActive(true);
    }

    // Save 버튼 클릭 시
    public void SaveButtonClicked()
    {
        // 현재 레벨을 GameManager에 저장
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        gameManager.SaveGame(currentLevel);

        // 게임 종료 처리
        gameManager.ExitGame();
    }
}
