using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // ���� ���� ���� (����, �÷��̾� ���� ��)
    public void SaveGame(int currentLevel)
    {
        PlayerPrefs.SetInt("CurrentLevel", currentLevel); // ���� ���� ����
        PlayerPrefs.Save(); // ����
        Debug.Log("���� �����, ����: " + currentLevel);
    }

    // ����� ���� ���� �ҷ�����
    public int LoadGame()
    {
        int savedLevel = PlayerPrefs.GetInt("CurrentLevel", 1); // �⺻�� 1
        return savedLevel;
    }

    // ���� ���� ó��
    public void ExitGame()
    {
        PlayerPrefs.Save(); // ���� ����
        Application.Quit(); // ���� ����
    }

    // ���� ���� �� ����� ���� �ҷ�����
    public void StartGame()
    {
        int savedLevel = LoadGame(); // ����� ������ �ҷ���
        LoadLevel(savedLevel); // �ش� ������ �ε�
    }

    // Ư�� ������ �� ��ȯ
    private void LoadLevel(int level)
    {
        SceneManager.LoadScene(level); // ����� ������ �� �ε�
    }
}
