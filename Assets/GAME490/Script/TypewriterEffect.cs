using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TypewriterEffect : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public AudioClip typingSound;
    public Button nextButton;     // �ؽ�Ʈ ���� �� ���̰�
    public Button skipButton;     // �ؽ�Ʈ ��� �߿� �̸� ���̰�
    public Button saveButton;     // �ؽ�Ʈ ���� �� ���̰� (����)
    public Button quitButton;     // �ؽ�Ʈ ���� �� ���̰� (����)
    public float typingSpeed = 0.1f;
    public float cursorBlinkSpeed = 0.5f;

    [TextArea(3, 10)]
    public string fullText = "";
    private string currentText = "";
    private AudioSource audioSource;
    private bool showCursor = true;
    private bool isTextComplete = false;

    void OnEnable()
    {
        // ������
        Debug.Log("Typewriter ����: " + gameObject.name);
        Debug.Log("�ؽ�Ʈ ����: " + fullText.Length);

        audioSource = GetComponent<AudioSource>();

        currentText = "";
        isTextComplete = false;
        showCursor = true;

        // ��ư �����
        if (nextButton != null) nextButton.gameObject.SetActive(false);
        if (skipButton != null) skipButton.gameObject.SetActive(false);
        if (saveButton != null) saveButton.gameObject.SetActive(false);
        if (quitButton != null) quitButton.gameObject.SetActive(false);

        StartCoroutine(ShowText());
        StartCoroutine(BlinkCursor());
    }

    IEnumerator ShowText()
    {
        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            textComponent.text = currentText + (showCursor ? "|" : "");

            // ���� ���� �� �� skip ��ư ǥ��
            if (i == 1 && skipButton != null)
            {
                skipButton.gameObject.SetActive(true);
            }

            // Ÿ���� ����
            if (typingSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(typingSound);
            }

            yield return new WaitForSeconds(typingSpeed);
        }

        isTextComplete = true;
        showCursor = false;

        // ��ư�� ǥ��
        if (nextButton != null) nextButton.gameObject.SetActive(true);
        if (saveButton != null) saveButton.gameObject.SetActive(true);
        if (quitButton != null) quitButton.gameObject.SetActive(true);
        if (skipButton != null) skipButton.gameObject.SetActive(false);

        textComponent.text = currentText;
    }

    IEnumerator BlinkCursor()
    {
        while (!isTextComplete)
        {
            showCursor = !showCursor;
            textComponent.text = currentText + (showCursor ? "|" : "");
            yield return new WaitForSeconds(cursorBlinkSpeed);
        }
    }

    public void Skip()
    {
        StopAllCoroutines();

        isTextComplete = true;
        showCursor = false;
        currentText = fullText;
        textComponent.text = fullText;

        if (nextButton != null) nextButton.gameObject.SetActive(true);
        if (saveButton != null) saveButton.gameObject.SetActive(true);
        if (quitButton != null) quitButton.gameObject.SetActive(true);
        if (skipButton != null) skipButton.gameObject.SetActive(false);
    }
}
