using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void Quit()
    {
        // ���� ����
        Application.Quit();

        // Unity �����Ϳ��� ���� ���̶�� �÷��� ��� ����
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
