using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public GameObject DialoguePanel;
    public Text DialogueText;
    public Button NextButton; // ���� ��� ��ư
    public string[] dialogue; // ��� �迭
    private int index;

    public float wordSpeed = 0.1f; // ���� ��� �ӵ�
    public bool playerIsClose;

    private bool isTyping = false; // ���� Ÿ���� ������ Ȯ��
    private Coroutine typingCoroutine; // ���� ���� ���� �ڷ�ƾ ���� ����

    void Start()
    {
        DialoguePanel.SetActive(false); // �ʱ� ���¿��� ��ȭâ �����
        NextButton.onClick.AddListener(NextLine); // ��ư Ŭ�� �̺�Ʈ �߰�
        NextButton.gameObject.SetActive(false); // ��ư ��Ȱ��ȭ
    }

    void Update()
    {
        // ��ȭ �г��� Ȱ��ȭ�ǰ�, ��簡 ���� ��� ��ư Ȱ��ȭ
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
        if (typingCoroutine != null) // ���� ���� �ڷ�ƾ�� ������ �ߴ�
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }

        DialogueText.text = ""; // �ؽ�Ʈ �ʱ�ȭ
        index = 0; // ��� �ε��� �ʱ�ȭ
        DialoguePanel.SetActive(false); // ��ȭ ���� �� ��ȭ �г� ����
        isTyping = false;
    }

    IEnumerator Typing()
    {
        isTyping = true; // Ÿ���� ����
        DialogueText.text = ""; // �ؽ�Ʈ �ʱ�ȭ
        string currentDialogue = dialogue[index]; // ���� ��� ��������

        foreach (char letter in currentDialogue.ToCharArray())
        {
            DialogueText.text += letter; // �� ���ھ� �߰�
            yield return new WaitForSeconds(wordSpeed);
        }

        isTyping = false; // Ÿ���� �Ϸ�
    }

    public void NextLine()
    {
        if (isTyping) // Ÿ���� ���� �� ��ư�� ������ Ÿ���� ��� �Ϸ�
        {
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine); // ���� �ڷ�ƾ ����
            }
            DialogueText.text = dialogue[index]; // ��縦 ��� �ϼ�
            isTyping = false; // Ÿ���� �Ϸ� ���·� ��ȯ
        }
        else if (index < dialogue.Length - 1) // Ÿ������ ���� ��� ���� ���� ����
        {
            index++;
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine); // ���� �ڷ�ƾ ����
            }
            typingCoroutine = StartCoroutine(Typing()); // ���� ��� ��� ����
        }
        else if (index >= dialogue.Length - 1) // ������ ����� ���
        {
            zeroText(); // ��ȭ ����
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true; // �÷��̾ ��ȭ ������ ����
            DialoguePanel.SetActive(true); // ��ȭ �г� Ȱ��ȭ
            index = 0; // ��縦 ó������ ����

            if (typingCoroutine != null) // ���� �ڷ�ƾ ����
            {
                StopCoroutine(typingCoroutine);
            }
            typingCoroutine = StartCoroutine(Typing()); // ù ��° ��� ��� ����
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false; // �÷��̾ ��ȭ �������� ����
            zeroText(); // ��ȭ ���� �� �ʱ�ȭ
        }
    }
}