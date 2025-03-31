using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TypewriterEffect : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public AudioClip typingSound;
    public Button nextButton;     // 텍스트 끝난 후 보이게
    public Button skipButton;     // 텍스트 출력 중에 미리 보이게
    public Button saveButton;     // 텍스트 끝난 후 보이게
    public Button quitButton;     // 텍스트 끝난 후 보이게
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

        // 시작할 때 모든 버튼 숨기기
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

            // 일정 글자 수 후 skip 버튼 표시
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

    // [선택] Skip 버튼 눌렀을 때 바로 전체 텍스트 출력
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
