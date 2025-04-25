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
    public Button saveButton;     // 텍스트 끝난 후 보이게 (선택)
    public Button quitButton;     // 텍스트 끝난 후 보이게 (선택)
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
        // 디버깅용
        Debug.Log("Typewriter 시작: " + gameObject.name);
        Debug.Log("텍스트 길이: " + fullText.Length);

        audioSource = GetComponent<AudioSource>();

        currentText = "";
        isTextComplete = false;
        showCursor = true;

        // 버튼 숨기기
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

            // 일정 글자 수 후 skip 버튼 표시
            if (i == 1 && skipButton != null)
            {
                skipButton.gameObject.SetActive(true);
            }

            // 타이핑 사운드
            if (typingSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(typingSound);
            }

            yield return new WaitForSeconds(typingSpeed);
        }

        isTextComplete = true;
        showCursor = false;

        // 버튼들 표시
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
