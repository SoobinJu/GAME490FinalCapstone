using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public GameObject DialoguePanel;
    public Text DialogueText;
    public string[] dialogue;
    private int index;

    public float wordSpeed = 0.05f;
    public bool playerIsClose = false;
    public float timeBetweenLines = 2f;

    private bool isTyping = false;
    private bool isDialogueFinished = false;
    private bool skipRequested = false;

    public Button NextButton;
    public Button SkipButton; // ✅ Skip 버튼 추가

    void Start()
    {
        if (DialoguePanel == null || DialogueText == null || NextButton == null || SkipButton == null)
        {
            Debug.LogError("Missing reference(s) in the NPC script.");
            return;
        }

        DialoguePanel.SetActive(false);
        NextButton.gameObject.SetActive(false);
        SkipButton.gameObject.SetActive(false); // 시작 시 숨김

        NextButton.onClick.AddListener(NextLine);
        SkipButton.onClick.AddListener(() => skipRequested = true);
    }

    void Update()
    {
        if (playerIsClose && DialoguePanel.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (isTyping)
                {
                    skipRequested = true;
                }
                else if (isDialogueFinished)
                {
                    NextLine();
                }
            }

            // Next 버튼은 대화 끝났을 때만 활성화
            if (isDialogueFinished)
            {
                NextButton.gameObject.SetActive(true);
            }
            else
            {
                NextButton.gameObject.SetActive(false);
            }
        }
    }

    public void zeroText()
    {
        if (DialogueText != null)
        {
            DialogueText.text = "";
        }

        index = 0;
        DialoguePanel.SetActive(false);
        NextButton.gameObject.SetActive(false);
        SkipButton.gameObject.SetActive(false);
    }

    IEnumerator Typing()
    {
        if (DialogueText == null || NextButton == null || SkipButton == null)
            yield break;

        isTyping = true;
        isDialogueFinished = false;
        skipRequested = false;
        DialogueText.text = "";

        SkipButton.gameObject.SetActive(false); // 시작할 때 항상 숨기기

        if (index >= 0 && index < dialogue.Length)
        {
            string currentLine = dialogue[index];

            for (int i = 0; i < currentLine.Length; i++)
            {
                if (skipRequested)
                {
                    DialogueText.text = currentLine;
                    break;
                }

                DialogueText.text += currentLine[i];

                // ✅ 글자 5개쯤 출력되면 Skip 버튼 보여줌
                if (i == 8)
                {
                    SkipButton.gameObject.SetActive(true);
                }

                yield return new WaitForSeconds(wordSpeed);
            }
        }

        isTyping = false;
        isDialogueFinished = true;
        skipRequested = false;

        SkipButton.gameObject.SetActive(false); // ✅ 문장 끝나면 Skip 버튼 숨김
        NextButton.gameObject.SetActive(true);  // ✅ 다음 버튼 활성화
    }

    public void NextLine()
    {
        if (!isTyping && index < dialogue.Length - 1)
        {
            index++;
            StartCoroutine(Typing());
            NextButton.gameObject.SetActive(false);
        }
        else if (index >= dialogue.Length - 1)
        {
            zeroText();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
            DialoguePanel.SetActive(true);
            index = 0;
            StopAllCoroutines();
            DialogueText.text = "";
            StartCoroutine(Typing());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            zeroText();
        }
    }

    public void RestartDialogue()
    {
        if (playerIsClose)
        {
            DialoguePanel.SetActive(true);
            index = 0;
            StopAllCoroutines();
            DialogueText.text = "";
            StartCoroutine(Typing());
        }
    }
}
