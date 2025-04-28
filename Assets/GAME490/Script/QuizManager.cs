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

    // 접근 제한자 수정됨!
    public void ShowQuiz()
    {
        if (quizCompleted)
        {
            Debug.Log("퀴즈 완료됨! 더이상 진행 불가.");
            DisableAllLetterButtons();
            return;
        }

        if (currentQuizIndex >= quizList.Count)
        {
            Debug.Log("퀴즈 끝! 다시 처음부터 시작합니다.");
            currentQuizIndex = 0;
            ShowQuiz();
            return;
        }

        QuizData quiz = quizList[currentQuizIndex];
        questionImage.sprite = quiz.image;
        currentAnswer = quiz.answer;

        Debug.Log("현재 문제 인덱스: " + currentQuizIndex + ", 정답: " + currentAnswer);

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

        // 다양한 자음 시작 랜덤 글자 조합
        string hangulCandidates = "가각간감같개거건검것고공과관구군권귀규그근글기김깊"
                                + "나낙난남내넌널넘네노놀누눈늘니님다단달담당대더던데도돈돌동두둘뒤드득들디따딸떠떻떼또뚜라락란람랑러런럼럽레로록론롱루류른를리린림마막만말망매맥맹머먹멀메며면명모목몰몸못무문물미민밀바박반받발밤방배백버번벌범법베벤병보복본볼봉부북분불붕붙비빈빌빚빠빨빵사삭산살삼상새색생서석선설섭세센셋소손솔송수숙순술숨숫시식신실심싣싶싸싹싼쌀쌍써썩썰쏘쓰쓴쓸씨씩씹씻아악안알암압앞애액앤야약얕얗얘언얼엄업없억에엔여역연열염엽영예오옥온올옮옷와완왜외요욕용우운울움웃워원위유윤으은을음응의이익인일읽임입잇있잎자작잔잘잠잡장재잭저적전절점접정젖제조족존졸좀종좌죄죠주죽준줄줌중즉즐증지직진질짐집짓짜짝짧째쩌쩍쩔쪼쪽쭉찌찍찐찢차착찬찰참창찾채책처척천철첫첩청체초촉촌총최추축춘출춤충취치칙친칠침칫카칸칼캐캠커컨컵켜코콘콜콩쿠쿨큐크큰클타탁탄탈탐탕태택터턱턴털테토통퇴투툴튜트튼틀티틱팀파판팔패팬퍼페편평폐포폭폰표푸풀품풍피픽핀필핏하학한할함항해핵핸햇행향허헌헐험헤헬혁현혈협형혜호혹혼홀홍화확환활황회획효후훈훌휘휴흑흔흘흡흥흩희흰히힘";

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
            Debug.Log("퀴즈 완료되어 더 이상 선택 불가!");
            return;
        }

        if (c == currentAnswer[currentLetterIndex])
        {
            letterSlots[currentLetterIndex].text = c.ToString();
            currentLetterIndex++;

            if (currentLetterIndex >= currentAnswer.Length)
            {
                Debug.Log("정답 완성!");
                StartCoroutine(ShowFeedback(true));
            }
        }
        else
        {
            Debug.Log("틀린 글자 선택!");
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
            Debug.Log("퀴즈 다 풀었음! EvaluateResult 호출 준비됨.");
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
