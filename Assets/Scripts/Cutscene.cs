using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI; // UI ������Ʈ ���

public class Cutscene : MonoBehaviour
{
    public TextMeshProUGUI textComponent; // TextMeshPro UI ������Ʈ
    public string[] lines; // ����� ��� ���
    public Sprite[] images; // ���� ����� �̹��� ���
    public Image displayImage; // UI���� �̹����� ǥ���ϴ� Image ������Ʈ

    public AudioSource audioSource; // ����� �ҽ� ������Ʈ
    public AudioClip[] soundEffects; // ���� ����� ���� ����Ʈ ���

    private int index; // ���� ��� ���� ������ �ε���
    private float lineDuration = 3f; // �� ����� ���� �ð� (3��)
    private float postTypingDelay = 2f; // Ÿ���� �� ��� �ð� (2��)

    // Start is called before the first frame update
    private void Start()
    {
        textComponent.text = string.Empty; // �ʱ� �ؽ�Ʈ�� ����
        StartDialogue();
    }

    void StartDialogue()
    {
        index = 0; // ù ��° ������ ����
        StartCoroutine(PlayDialogue());
    }

    IEnumerator PlayDialogue()
    {
        while (index < lines.Length)
        {
            textComponent.text = string.Empty; // �ؽ�Ʈ �ʱ�ȭ
            UpdateImage(); // �̹��� ������Ʈ
            PlaySoundEffect(); // ���� ����Ʈ ���
            yield return StartCoroutine(TypeLine()); // ���� ������ ���
            yield return new WaitForSeconds(lineDuration - postTypingDelay); // Ÿ���� �ð� ������ ���
            NextLine(); // ���� �������� �̵�
        }
        LoadNextScene(); // ��ȭ�� ������ �� ��ȯ
    }

    IEnumerator TypeLine()
    {
        string line = lines[index]; // ���� ���
        float charDisplayTime = postTypingDelay / line.Length; // �� ���ڴ� ��� �ð�

        foreach (char c in line.ToCharArray())
        {
            textComponent.text += c; // �� ���ھ� �߰�
            yield return new WaitForSeconds(charDisplayTime);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++; // ���� �������� �̵�
        }
        else
        {
            index = lines.Length; // ��ȭ�� �������� ǥ��
        }
    }

    void UpdateImage()
    {
        if (index < images.Length && displayImage != null)
        {
            displayImage.sprite = images[index]; // ���� �ε����� �´� �̹����� ����
        }
    }

    void PlaySoundEffect()
    {
        if (index < soundEffects.Length && audioSource != null && soundEffects[index] != null)
        {
            audioSource.PlayOneShot(soundEffects[index]); // ���� ��翡 �´� ���� ����Ʈ ���
        }
    }

    // ���� ������ �̵��ϴ� �Լ�
    void LoadNextScene()
    {
        SceneManager.LoadScene("stage1");
    }
}
