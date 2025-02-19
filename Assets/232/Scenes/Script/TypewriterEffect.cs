using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TypewriterEffect : MonoBehaviour
{
    public TextMeshProUGUI textComponent; // 연결할 TextMeshPro 오브젝트
    public AudioClip typingSound;         // 타자기 소리
    public Button[] buttons;             // 여러 버튼 배열
    public float typingSpeed = 0.1f;      // 글씨가 나오는 속도 (초)
    public float cursorBlinkSpeed = 0.5f; // 커서 깜박이는 속도 (초)

    [TextArea(3, 10)]
    public string fullText = "";          // Inspector에서 설정 가능한 전체 텍스트
    private string currentText = "";      // 현재 표시된 텍스트
    private AudioSource audioSource;      // 오디오 소스
    private bool showCursor = true;       // 커서 깜박임 상태
    private bool isTextComplete = false;  // 텍스트 출력 완료 여부

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // AudioSource 컴포넌트 가져오기

        // 버튼들 비활성화
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
            currentText = fullText.Substring(0, i); // i번째까지 텍스트 가져오기
            textComponent.text = currentText + (showCursor ? "|" : ""); // 커서 추가

            // 타자기 소리 재생
            if (typingSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(typingSound);
            }

            yield return new WaitForSeconds(typingSpeed); // 대기 시간
        }

        isTextComplete = true; // 텍스트 출력 완료

        // 버튼들 활성화
        foreach (var button in buttons)
        {
            button.gameObject.SetActive(true);
        }

        textComponent.text = currentText; // 커서를 제거
    }

    IEnumerator BlinkCursor()
    {
        while (!isTextComplete) // 텍스트 출력 중에만 커서 깜박임
        {
            showCursor = !showCursor; // 커서 상태 반전
            textComponent.text = currentText + (showCursor ? "|" : ""); // 텍스트 갱신
            yield return new WaitForSeconds(cursorBlinkSpeed); // 깜박임 속도
        }
    }
}
