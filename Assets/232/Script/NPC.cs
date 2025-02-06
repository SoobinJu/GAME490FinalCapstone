using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public GameObject DialoguePanel;
    public Text DialogueText;
    public Button NextButton; // 다음 대사 버튼
    public string[] dialogue; // 대사 배열
    private int index;

    public float wordSpeed = 0.1f; // 글자 출력 속도
    public bool playerIsClose;

    private bool isTyping = false; // 현재 타이핑 중인지 확인
    private Coroutine typingCoroutine; // 현재 진행 중인 코루틴 참조 저장

    void Start()
    {
        DialoguePanel.SetActive(false); // 초기 상태에서 대화창 숨기기
        NextButton.onClick.AddListener(NextLine); // 버튼 클릭 이벤트 추가
        NextButton.gameObject.SetActive(false); // 버튼 비활성화
    }

    void Update()
    {
        // 대화 패널이 활성화되고, 대사가 끝난 경우 버튼 활성화
        if (DialoguePanel.activeInHierarchy && !isTyping && index < dialogue.Length - 1)
        {
            NextButton.gameObject.SetActive(true);
        }
        else
        {
            NextButton.gameObject.SetActive(false);
        }
    }

    public void zeroText()
    {
        if (typingCoroutine != null) // 진행 중인 코루틴이 있으면 중단
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }

        DialogueText.text = ""; // 텍스트 초기화
        index = 0; // 대사 인덱스 초기화
        DialoguePanel.SetActive(false); // 대화 종료 시 대화 패널 끄기
        isTyping = false;
    }

    IEnumerator Typing()
    {
        isTyping = true; // 타이핑 시작
        DialogueText.text = ""; // 텍스트 초기화
        string currentDialogue = dialogue[index]; // 현재 대사 가져오기

        foreach (char letter in currentDialogue.ToCharArray())
        {
            DialogueText.text += letter; // 한 글자씩 추가
            yield return new WaitForSeconds(wordSpeed);
        }

        isTyping = false; // 타이핑 완료
    }

    public void NextLine()
    {
        if (isTyping) // 타이핑 중일 때 버튼이 눌리면 타이핑 즉시 완료
        {
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine); // 현재 코루틴 중지
            }
            DialogueText.text = dialogue[index]; // 대사를 즉시 완성
            isTyping = false; // 타이핑 완료 상태로 전환
        }
        else if (index < dialogue.Length - 1) // 타이핑이 끝난 경우 다음 대사로 진행
        {
            index++;
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine); // 이전 코루틴 중지
            }
            typingCoroutine = StartCoroutine(Typing()); // 다음 대사 출력 시작
        }
        else if (index >= dialogue.Length - 1) // 마지막 대사일 경우
        {
            zeroText(); // 대화 종료
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true; // 플레이어가 대화 범위에 들어옴
            DialoguePanel.SetActive(true); // 대화 패널 활성화
            index = 0; // 대사를 처음부터 시작

            if (typingCoroutine != null) // 이전 코루틴 중지
            {
                StopCoroutine(typingCoroutine);
            }
            typingCoroutine = StartCoroutine(Typing()); // 첫 번째 대사 출력 시작
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false; // 플레이어가 대화 범위에서 나감
            zeroText(); // 대화 종료 및 초기화
        }
    }
}