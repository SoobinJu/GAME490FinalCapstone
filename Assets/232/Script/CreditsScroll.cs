using UnityEngine;
using UnityEngine.UI;

public class CreditsScroll : MonoBehaviour
{
    public RectTransform creditsText; // �ؽ�Ʈ�� RectTransform
    public float scrollSpeed = 50f;   // ��ũ�� �ӵ� (�ȼ�/��)
    public float endPositionY = 1000f; // �ؽ�Ʈ�� ������ Y ��ǥ
    public Button[] buttons;          // ��Ÿ�� ��ư��

    private bool isCreditsComplete = false; // ũ���� �Ϸ� ����

    void Start()
    {
        // ��ư�� ��Ȱ��ȭ
        foreach (var button in buttons)
        {
            button.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (!isCreditsComplete)
        {
            // ���� �̵�
            creditsText.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;

            // �ؽ�Ʈ�� ������ �ö��� ��
            if (creditsText.anchoredPosition.y >= endPositionY)
            {
                isCreditsComplete = true;
                ShowButtons(); // ��ư ǥ��
            }
        }
    }

    void ShowButtons()
    {
        foreach (var button in buttons)
        {
            button.gameObject.SetActive(true); // ��ư Ȱ��ȭ
        }
    }
}
