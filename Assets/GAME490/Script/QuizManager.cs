using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class QuizData
{
    public Sprite image;
    public string answer;
}

public class QuizManager : MonoBehaviour
{
    public List<QuizData> quizList;
    public Image questionImage;
    public TextMeshProUGUI[] letterSlots;
    public GameObject letterButtonPrefab;
    public Transform buttonParent;

    private string currentAnswer;
    private int currentQuizIndex = 0;
    private int currentLetterIndex = 0;

    public Image correctImage;
    public Image wrongImage;

    public QuizChance quizChance;
    public TextMeshProUGUI resultText;
    private bool[] isCorrectList;

    private bool quizCompleted = false;

    void Awake()
    {
        Time.timeScale = 1f;
    }

    void Start()
    {
        isCorrectList = new bool[quizList.Count];
        ShowQuiz();
    }

    // ���� ������ ������!
    public void ShowQuiz()
    {
        if (quizCompleted)
        {
            Debug.Log("���� �Ϸ��! ���̻� ���� �Ұ�.");
            DisableAllLetterButtons();
            return;
        }

        if (currentQuizIndex >= quizList.Count)
        {
            Debug.Log("���� ��! �ٽ� ó������ �����մϴ�.");
            currentQuizIndex = 0;
            ShowQuiz();
            return;
        }

        QuizData quiz = quizList[currentQuizIndex];
        questionImage.sprite = quiz.image;
        currentAnswer = quiz.answer;

        Debug.Log("���� ���� �ε���: " + currentQuizIndex + ", ����: " + currentAnswer);

        for (int i = 0; i < letterSlots.Length; i++)
        {
            if (i < currentAnswer.Length)
            {
                letterSlots[i].text = "_";
                letterSlots[i].gameObject.SetActive(true);
            }
            else
            {
                letterSlots[i].text = "";
                letterSlots[i].gameObject.SetActive(false);
            }
        }

        currentLetterIndex = 0;
        GenerateLetterButtons();
    }

    void GenerateLetterButtons()
    {
        foreach (Transform child in buttonParent)
        {
            Destroy(child.gameObject);
        }

        List<char> letterOptions = new List<char>(currentAnswer.ToCharArray());

        // �پ��� ���� ���� ���� ���� ����
        string hangulCandidates = "�������������Űǰ˰Ͱ�����������ǱͱԱױٱ۱���"
                                + "�����������ͳγѳ׳����ôϴԴٴܴ޴�����������������εѵڵ��������������ǶѶ�������������������ηϷзշ�����������������������ŸƸ͸ӸԸָ޸�����������������̹ιйٹڹݹ޹߹������������������������������κϺкҺغٺ��������������������������������������¼Ҽռּۼ������������ýĽŽǽɽƽͽνϽѽҽֽ������������þľƾǾȾ˾Ͼоվ־׾ؾ߾������������￡�����������������������¿ÿſʿͿϿֿܿ����������������������������������������������������������������������������������������������������������������������������������������¥¦ª°¼½¿������������������������âãäåóôõöùøûü������������������������ġĢģĥħĩīĭĮĳķĿ������������������ťũūŬŸŹźŻŽ��������������������������ƩƮưƲƼƽ����������������������������ǥǪǮǰǳ����������������������������������������������������������ȣȤȥȦȫȭȮȯȰȲȸȹȿ������������������������������";

        while (letterOptions.Count < 8)
        {
            char randomChar = hangulCandidates[Random.Range(0, hangulCandidates.Length)];
            if (!letterOptions.Contains(randomChar))
                letterOptions.Add(randomChar);
        }

        letterOptions = letterOptions.OrderBy(a => Random.value).ToList();

        foreach (char c in letterOptions)
        {
            GameObject btn = Instantiate(letterButtonPrefab, buttonParent);
            btn.GetComponentInChildren<TextMeshProUGUI>().text = c.ToString();

            char selectedChar = c;
            btn.GetComponent<Button>().onClick.AddListener(() => OnLetterClick(selectedChar));
        }
    }

    void OnLetterClick(char c)
    {
        if (quizCompleted)
        {
            Debug.Log("���� �Ϸ�Ǿ� �� �̻� ���� �Ұ�!");
            return;
        }

        if (c == currentAnswer[currentLetterIndex])
        {
            letterSlots[currentLetterIndex].text = c.ToString();
            currentLetterIndex++;

            if (currentLetterIndex >= currentAnswer.Length)
            {
                Debug.Log("���� �ϼ�!");
                StartCoroutine(ShowFeedback(true));
            }
        }
        else
        {
            Debug.Log("Ʋ�� ���� ����!");
            StartCoroutine(ShowFeedback(false));
        }
    }

    IEnumerator ShowFeedback(bool isCorrect)
    {
        if (isCorrect)
        {
            correctImage.gameObject.SetActive(true);
            isCorrectList[currentQuizIndex] = true;
        }
        else
        {
            wrongImage.gameObject.SetActive(true);
            isCorrectList[currentQuizIndex] = false;
        }

        yield return new WaitForSecondsRealtime(1f);

        correctImage.gameObject.SetActive(false);
        wrongImage.gameObject.SetActive(false);

        NextQuiz();
    }

    void NextQuiz()
    {
        currentLetterIndex = 0;
        currentQuizIndex++;

        if (currentQuizIndex < quizList.Count)
        {
            ShowQuiz();
        }
        else
        {
            Debug.Log("���� �� Ǯ����! EvaluateResult ȣ�� �غ��.");
            EvaluateResult();
        }
    }

    void EvaluateResult()
    {
        bool allCorrect = isCorrectList.All(correct => correct);

        if (allCorrect)
        {
            resultText.text = "SUCCESS!";
            quizChance.IncreaseChances();
        }
        else
        {
            resultText.text = "Oops. Keep going!";
        }

        resultText.gameObject.SetActive(true);
        quizCompleted = true;
        DisableAllLetterButtons();
    }

    void DisableAllLetterButtons()
    {
        foreach (Transform child in buttonParent)
        {
            Button btn = child.GetComponent<Button>();
            if (btn != null)
            {
                btn.interactable = false;
            }
        }
    }

    public bool IsQuizCompleted()
    {
        return quizCompleted;
    }
}
