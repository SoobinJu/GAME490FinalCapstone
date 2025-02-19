using UnityEngine;
using UnityEngine.UI;

public class CreditsScroll : MonoBehaviour
{
    public RectTransform creditsText; // 텍스트의 RectTransform
    public float scrollSpeed = 50f;   // 스크롤 속도 (픽셀/초)
    public float endPositionY = 1000f; // 텍스트가 끝나는 Y 좌표
    public Button[] buttons;          // 나타낼 버튼들

    private bool isCreditsComplete = false; // 크레딧 완료 여부

    void Start()
    {
        // 버튼들 비활성화
        foreach (var button in buttons)
        {
            button.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (!isCreditsComplete)
        {
            // 위로 이동
            creditsText.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;

            // 텍스트가 끝까지 올라갔을 때
            if (creditsText.anchoredPosition.y >= endPositionY)
            {
                isCreditsComplete = true;
                ShowButtons(); // 버튼 표시
            }
        }
    }

    void ShowButtons()
    {
        foreach (var button in buttons)
        {
            button.gameObject.SetActive(true); // 버튼 활성화
        }
    }
}
