using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void Quit()
    {
        // 게임 종료
        Application.Quit();

        // Unity 에디터에서 실행 중이라면 플레이 모드 종료
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
