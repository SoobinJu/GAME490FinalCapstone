using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimedPanelController : MonoBehaviour
{
    public GameObject targetPanel;      // 관리할 패널
    public Text timerText;              // 남은 시간 표시용 텍스트
    public float timeLimit = 61f;       // 공통 제한 시간 (예: 1분 30초)
    private float timer;
    private bool isTimerRunning = false;

    public QuizChance quizChance;  // 기회 관리 스크립트 연결
    public Text timeOutText;  //  Time Out! 텍스트 연결

    private bool wasTimedOut = false;  // 타임아웃 상태 저장


    public AudioClip timeUpSound;
    public AudioSource audioSource;

    void Start()
    {
        StartTimer();  // 일단 테스트용으로 자동 시작되게 해보자!
    }


    void Update()
    {
        if (isTimerRunning)
        {
            timer -= Time.unscaledDeltaTime;

            if (timer <= 0.01f)  // 더 여유 있게, 확실하게!
            {
                timer = 0;
                isTimerRunning = false;
                timerText.text = "0:00";
                Debug.Log("타이머 0 도달 → 타임아웃 처리 호출!");
                OnTimeLimitReached();  // 무조건 호출됨!
            }
            else
            {
                int minutes = Mathf.FloorToInt(timer / 60f);
                int seconds = Mathf.FloorToInt(timer % 60f);
                timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
            }
        }
    }



    // 패널 열릴 때 타이머 시작
    public void StartTimer()
    {
        Debug.Log("StartTimer 호출됨 → 타이머 리셋 및 작동 시작!");

        timer = timeLimit;       // 시간 리셋 (예: 61초)
        isTimerRunning = true;   // 타이머 작동 플래그 true
        timerText.gameObject.SetActive(true);  // 텍스트 보이게

        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);
        timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);

        Debug.Log("StartTimer 내부 → timer = " + timer + ", isTimerRunning = " + isTimerRunning);
    }



    // 패널 닫힐 때 타이머 정지 (텍스트는 그대로)
    public void StopTimer()
    {
        isTimerRunning = false;
        // timerText.gameObject.SetActive(false); //  텍스트 숨기지 않음
    }

    private IEnumerator ClosePanelAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);  //  일시정지 무시하고 기다림

        targetPanel.SetActive(false);  // 패널 닫기

        if (timeOutText != null)
        {
            timeOutText.gameObject.SetActive(false);  // Time Out 텍스트 숨기기
        }
    }


    void OnTimeLimitReached()
    {
        Debug.Log(">>> OnTimeLimitReached 호출됨!!!");  // 이거 무조건 뜨나?

        wasTimedOut = true;
        Debug.Log(">>> wasTimedOut 값: " + wasTimedOut);  // true 찍히는지 확인

        if (timeUpSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(timeUpSound);
        }

        if (quizChance != null)
        {
            quizChance.StartDecreaseChances();
        }

        if (timeOutText != null)
        {
            timeOutText.gameObject.SetActive(true);
        }

        Time.timeScale = 1f;

        StartCoroutine(ClosePanelAfterDelay(1f));
    }

    public void OpenPanel()
    {
        targetPanel.SetActive(true);

        if (wasTimedOut)
        {
            StartTimer();      // 타이머 새로 시작
            wasTimedOut = false;  // 상태 초기화
        }
        else
        {
            timerText.gameObject.SetActive(true);  // 기존 타이머 유지
        }
    }






}