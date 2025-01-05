using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent; // TextMeshPro UI ������Ʈ
    public string[] lines; // ����� ��� ���

    private int index; // ���� ��� ���� ������ �ε���
    private float lineDuration = 2f; // �� ����� ���� �ð� (2��)
    private float postTypingDelay = 0.5f; // Ÿ���� �� ��� �ð� (0.5��)

    // Start is called before the first frame update
    private void Start()
    {
        textComponent.text = string.Empty; // �ʱ� �ؽ�Ʈ�� ����
        StartDialogue();
    }

    void StartDialogue()
    {
        index = 0; // ù ��° ������ ����
        StartCoroutine(PlayDialogue());
    }

    IEnumerator PlayDialogue()
    {
        while (index < lines.Length)
        {
            textComponent.text = string.Empty; // �ؽ�Ʈ �ʱ�ȭ
            yield return StartCoroutine(TypeLine()); // ���� ������ ���
            yield return new WaitForSeconds(lineDuration - postTypingDelay); // Ÿ���� �ð� ������ ���
            NextLine(); // ���� �������� �̵�
        }
        LoadNextScene(); // ��ȭ�� ������ �� ��ȯ
    }

    IEnumerator TypeLine()
    {
        string line = lines[index]; // ���� ���
        float charDisplayTime = postTypingDelay / line.Length; // �� ���ڴ� ��� �ð�

        foreach (char c in line.ToCharArray())
        {
            textComponent.text += c; // �� ���ھ� �߰�
            yield return new WaitForSeconds(charDisplayTime);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++; // ���� �������� �̵�
        }
    }

    // ���� ������ �̵��ϴ� �Լ�
    void LoadNextScene()
    {
        SceneManager.LoadScene("stage1");
    }
}
