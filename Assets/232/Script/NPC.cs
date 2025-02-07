using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public GameObject DialoguePanel; // 이 NPC의 대화 패널
    public Text DialogueText;        // 이 NPC의 대화 텍스트
    public string[] dialogue;        // 이 NPC의 대화 내용
    private int index;

    public float wordSpeed = 0.05f;  // 글자 타이핑 속도
    public bool playerIsClose = false; // 플레이어가 가까이 있는지 체크
    public float timeBetweenLines = 2f; // 한 대화 라인 사이의 시간

    private bool isTyping = false;    // 현재 타이핑 중인지 체크
    private bool isDialogueFinished = false; // 대화가 끝났는지 체크

    public Button NextButton; // 다음 대화로 넘어가는 버튼

    void Start()
    {
        // 필요한 참조가 없으면 오류 로그 출력
        if (DialoguePanel == null || DialogueText == null || NextButton == null)
        {
            Debug.LogError("Missing reference(s) in the NPC script.");
            return;
        }

        // 초기 설정: 대화 패널과 버튼을 숨깁니다.
        DialoguePanel.SetActive(false);
        NextButton.gameObject.SetActive(false);
        NextButton.onClick.AddListener(NextLine); // 버튼 클릭 시 NextLine 메서드 실행
    }

    void Update()
    {
        // 플레이어가 가까이 있고 대화가 끝났을 때만 버튼을 보이게 합니다.
        if (playerIsClose && DialoguePanel.activeInHierarchy && isDialogueFinished)
        {
            NextButton.gameObject.SetActive(true); // 대화가 끝난 후 버튼 활성화
        }
    }

    public void zeroText()
    {
        // 대화 종료 시 초기화
        if (DialogueText != null)
        {
            DialogueText.text = "";
        }

        index = 0;
        DialoguePanel.SetActive(false); // 대화 종료 후 패널 숨기기
        NextButton.gameObject.SetActive(false); // 버튼도 숨기기
    }

    IEnumerator Typing()
    {
        // 참조가 없으면 바로 종료
        if (DialogueText == null || NextButton == null)
        {
            yield break;
        }

        isTyping = true;
        isDialogueFinished = false;

        DialogueText.text = ""; // 이전 대화 텍스트 지우기

        // 현재 대화 내용이 있으면 출력
        if (index >= 0 && index < dialogue.Length)
        {
            foreach (char letter in dialogue[index].ToCharArray())
            {
                DialogueText.text += letter;
                yield return new WaitForSeconds(wordSpeed); // 글자 타이핑 간격
            }
        }

        isTyping = false;
        isDialogueFinished = true;

        // 대화가 끝났을 때 버튼 활성화
        if (NextButton != null)
        {
            NextButton.gameObject.SetActive(true);
        }
    }

    public void NextLine()
    {
        // 타이핑 중이 아니고, 더 많은 대화가 있을 때
        if (!isTyping && index < dialogue.Length - 1)
        {
            index++;
            StartCoroutine(Typing()); // 다음 대화 출력
            if (NextButton != null)
            {
                NextButton.gameObject.SetActive(false); // 타이핑 중에는 버튼 숨기기
            }
        }
        else if (index >= dialogue.Length - 1) // 마지막 대화가 끝났을 때
        {
            zeroText(); // 대화 종료
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 플레이어가 NPC와 충돌하면 대화 시작
        if (other.CompareTag("Player") && !isTyping)
        {
            playerIsClose = true;
            DialoguePanel.SetActive(true); // 대화 패널 보이기
            index = 0; // 첫 번째 대화부터 시작
            StartCoroutine(Typing()); // 첫 번째 대화 라인 출력
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // 플레이어가 범위에서 벗어나면 대화 종료
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            zeroText(); // 대화 종료
        }
    }

    // 충돌 영역에 들어갈 때마다 대화를 다시 시작할 수 있게 해주는 메서드 추가
    public void RestartDialogue()
    {
        // 대화가 진행 중이지 않고, 플레이어가 근처에 있을 때만 대화 시작
        if (playerIsClose && !isTyping && index < dialogue.Length)
        {
            DialoguePanel.SetActive(true); // 대화 패널 보이기
            index = 0; // 첫 번째 대화부터 시작
            StartCoroutine(Typing()); // 첫 번째 대화 라인 출력
        }
    }
}
