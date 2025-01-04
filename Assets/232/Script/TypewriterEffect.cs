using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TypewriterEffect : MonoBehaviour
{
    public TextMeshProUGUI textComponent; // ������ TextMeshPro ������Ʈ
    public AudioClip typingSound;         // Ÿ�ڱ� �Ҹ�
    public Button[] buttons;             // ���� ��ư �迭
    public float typingSpeed = 0.1f;      // �۾��� ������ �ӵ� (��)
    public float cursorBlinkSpeed = 0.5f; // Ŀ�� �����̴� �ӵ� (��)

    [TextArea(3, 10)]
    public string fullText = "";          // Inspector���� ���� ������ ��ü �ؽ�Ʈ
    private string currentText = "";      // ���� ǥ�õ� �ؽ�Ʈ
    private AudioSource audioSource;      // ����� �ҽ�
    private bool showCursor = true;       // Ŀ�� ������ ����
    private bool isTextComplete = false;  // �ؽ�Ʈ ��� �Ϸ� ����

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // AudioSource ������Ʈ ��������

        // ��ư�� ��Ȱ��ȭ
        foreach (var button in buttons)
        {
            button.gameObject.SetActive(false);
        }

        StartCoroutine(ShowText());
        StartCoroutine(BlinkCursor());
    }

    IEnumerator ShowText()
    {
        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i); // i��°���� �ؽ�Ʈ ��������
            textComponent.text = currentText + (showCursor ? "|" : ""); // Ŀ�� �߰�

            // Ÿ�ڱ� �Ҹ� ���
            if (typingSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(typingSound);
            }

            yield return new WaitForSeconds(typingSpeed); // ��� �ð�
        }

        isTextComplete = true; // �ؽ�Ʈ ��� �Ϸ�

        // ��ư�� Ȱ��ȭ
        foreach (var button in buttons)
        {
            button.gameObject.SetActive(true);
        }

        textComponent.text = currentText; // Ŀ���� ����
    }

    IEnumerator BlinkCursor()
    {
        while (!isTextComplete) // �ؽ�Ʈ ��� �߿��� Ŀ�� ������
        {
            showCursor = !showCursor; // Ŀ�� ���� ����
            textComponent.text = currentText + (showCursor ? "|" : ""); // �ؽ�Ʈ ����
            yield return new WaitForSeconds(cursorBlinkSpeed); // ������ �ӵ�
        }
    }
}
