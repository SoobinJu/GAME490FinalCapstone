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
    public Button saveButton;     // �ؽ�Ʈ ���� �� ���̰�
    public Button quitButton;     // �ؽ�Ʈ ���� �� ���̰�
    public float typingSpeed = 0.1f;
    public float cursorBlinkSpeed = 0.5f;

    [TextArea(3, 10)]
    public string fullText = "";
    private string currentText = "";
    private AudioSource audioSource;
    private bool showCursor = true;
    private bool isTextComplete = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // ������ �� ��� ��ư �����
        nextButton.gameObject.SetActive(false);
        skipButton.gameObject.SetActive(false);
        saveButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);

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
            if (i == 20)
            {
                skipButton.gameObject.SetActive(true);
            }

            if (typingSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(typingSound);
            }

            yield return new WaitForSeconds(typingSpeed);
        }

        isTextComplete = true;
        showCursor = false;

        nextButton.gameObject.SetActive(true);

        if (saveButton != null)
            saveButton.gameObject.SetActive(true);

        if (quitButton != null)
            quitButton.gameObject.SetActive(true);

        skipButton.gameObject.SetActive(false);
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

    // [����] Skip ��ư ������ �� �ٷ� ��ü �ؽ�Ʈ ���
    public void Skip()
    {
        StopAllCoroutines();

        isTextComplete = true;
        showCursor = false;
        currentText = fullText;
        textComponent.text = fullText;

        nextButton.gameObject.SetActive(true);

        if (saveButton != null)
            saveButton.gameObject.SetActive(true);

        if (quitButton != null)
            quitButton.gameObject.SetActive(true);

        skipButton.gameObject.SetActive(false);
    }


}
