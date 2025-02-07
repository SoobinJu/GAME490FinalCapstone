using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public GameObject DialoguePanel; // �� NPC�� ��ȭ �г�
    public Text DialogueText;        // �� NPC�� ��ȭ �ؽ�Ʈ
    public string[] dialogue;        // �� NPC�� ��ȭ ����
    private int index;

    public float wordSpeed = 0.05f;  // ���� Ÿ���� �ӵ�
    public bool playerIsClose = false; // �÷��̾ ������ �ִ��� üũ
    public float timeBetweenLines = 2f; // �� ��ȭ ���� ������ �ð�

    private bool isTyping = false;    // ���� Ÿ���� ������ üũ
    private bool isDialogueFinished = false; // ��ȭ�� �������� üũ

    public Button NextButton; // ���� ��ȭ�� �Ѿ�� ��ư

    void Start()
    {
        // �ʿ��� ������ ������ ���� �α� ���
        if (DialoguePanel == null || DialogueText == null || NextButton == null)
        {
            Debug.LogError("Missing reference(s) in the NPC script.");
            return;
        }

        // �ʱ� ����: ��ȭ �гΰ� ��ư�� ����ϴ�.
        DialoguePanel.SetActive(false);
        NextButton.gameObject.SetActive(false);
        NextButton.onClick.AddListener(NextLine); // ��ư Ŭ�� �� NextLine �޼��� ����
    }

    void Update()
    {
        // �÷��̾ ������ �ְ� ��ȭ�� ������ ���� ��ư�� ���̰� �մϴ�.
        if (playerIsClose && DialoguePanel.activeInHierarchy && isDialogueFinished)
        {
            NextButton.gameObject.SetActive(true); // ��ȭ�� ���� �� ��ư Ȱ��ȭ
        }
    }

    public void zeroText()
    {
        // ��ȭ ���� �� �ʱ�ȭ
        if (DialogueText != null)
        {
            DialogueText.text = "";
        }

        index = 0;
        DialoguePanel.SetActive(false); // ��ȭ ���� �� �г� �����
        NextButton.gameObject.SetActive(false); // ��ư�� �����
    }

    IEnumerator Typing()
    {
        // ������ ������ �ٷ� ����
        if (DialogueText == null || NextButton == null)
        {
            yield break;
        }

        isTyping = true;
        isDialogueFinished = false;

        DialogueText.text = ""; // ���� ��ȭ �ؽ�Ʈ �����

        // ���� ��ȭ ������ ������ ���
        if (index >= 0 && index < dialogue.Length)
        {
            foreach (char letter in dialogue[index].ToCharArray())
            {
                DialogueText.text += letter;
                yield return new WaitForSeconds(wordSpeed); // ���� Ÿ���� ����
            }
        }

        isTyping = false;
        isDialogueFinished = true;

        // ��ȭ�� ������ �� ��ư Ȱ��ȭ
        if (NextButton != null)
        {
            NextButton.gameObject.SetActive(true);
        }
    }

    public void NextLine()
    {
        // Ÿ���� ���� �ƴϰ�, �� ���� ��ȭ�� ���� ��
        if (!isTyping && index < dialogue.Length - 1)
        {
            index++;
            StartCoroutine(Typing()); // ���� ��ȭ ���
            if (NextButton != null)
            {
                NextButton.gameObject.SetActive(false); // Ÿ���� �߿��� ��ư �����
            }
        }
        else if (index >= dialogue.Length - 1) // ������ ��ȭ�� ������ ��
        {
            zeroText(); // ��ȭ ����
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // �÷��̾ NPC�� �浹�ϸ� ��ȭ ����
        if (other.CompareTag("Player") && !isTyping)
        {
            playerIsClose = true;
            DialoguePanel.SetActive(true); // ��ȭ �г� ���̱�
            index = 0; // ù ��° ��ȭ���� ����
            StartCoroutine(Typing()); // ù ��° ��ȭ ���� ���
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // �÷��̾ �������� ����� ��ȭ ����
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            zeroText(); // ��ȭ ����
        }
    }

    // �浹 ������ �� ������ ��ȭ�� �ٽ� ������ �� �ְ� ���ִ� �޼��� �߰�
    public void RestartDialogue()
    {
        // ��ȭ�� ���� ������ �ʰ�, �÷��̾ ��ó�� ���� ���� ��ȭ ����
        if (playerIsClose && !isTyping && index < dialogue.Length)
        {
            DialoguePanel.SetActive(true); // ��ȭ �г� ���̱�
            index = 0; // ù ��° ��ȭ���� ����
            StartCoroutine(Typing()); // ù ��° ��ȭ ���� ���
        }
    }
}
