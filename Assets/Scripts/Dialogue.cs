using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent; // TextMeshPro UI 컴포넌트
    public string[] lines; // 출력할 대사 목록

    private int index; // 현재 출력 중인 라인의 인덱스
    private float lineDuration = 2f; // 각 대사의 지속 시간 (2초)
    private float postTypingDelay = 0.5f; // 타이핑 후 대기 시간 (0.5초)

    // Start is called before the first frame update
    private void Start()
    {
        textComponent.text = string.Empty; // 초기 텍스트를 비우기
        StartDialogue();
    }

    void StartDialogue()
    {
        index = 0; // 첫 번째 대사부터 시작
        StartCoroutine(PlayDialogue());
    }

    IEnumerator PlayDialogue()
    {
        while (index < lines.Length)
        {
            textComponent.text = string.Empty; // 텍스트 초기화
            yield return StartCoroutine(TypeLine()); // 현재 라인을 출력
            yield return new WaitForSeconds(lineDuration - postTypingDelay); // 타이핑 시간 제외한 대기
            NextLine(); // 다음 라인으로 이동
        }
        LoadNextScene(); // 대화가 끝나면 씬 전환
    }

    IEnumerator TypeLine()
    {
        string line = lines[index]; // 현재 대사
        float charDisplayTime = postTypingDelay / line.Length; // 한 글자당 출력 시간

        foreach (char c in line.ToCharArray())
        {
            textComponent.text += c; // 한 글자씩 추가
            yield return new WaitForSeconds(charDisplayTime);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++; // 다음 라인으로 이동
        }
    }

    // 다음 씬으로 이동하는 함수
    void LoadNextScene()
    {
        SceneManager.LoadScene("stage1");
    }
}
