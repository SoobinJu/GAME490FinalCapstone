using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameManager gameManager; // GameManager�� ����

    // ���� ���� �� ȣ��� �Լ� (�����̼� ȭ�鿡�� ��ư�� ǥ��)
    public void OnLevelEnd()
    {
        // Save ��ư�� Next ��ư�� ȭ�鿡 ǥ���ϴ� UI�� Ȱ��ȭ
        // ���� ���, saveButton.SetActive(true);
    }

    // Save ��ư Ŭ�� ��
    public void SaveButtonClicked()
    {
        // ���� ������ GameManager�� ����
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        gameManager.SaveGame(currentLevel);

        // ���� ���� ó��
        gameManager.ExitGame();
    }
}
