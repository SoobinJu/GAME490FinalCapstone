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
        Debug.Log(">>> Update() 작동중, 타이머 상태: " + timer + ", isTimerRunning: " + isTimerRunning);

        if (isTimerRunning)
        {
            timer -= Time.unscaledDeltaTime;

            if (timer <= 0.01f)
            {
                timer = 0;
                isTimerRunning = false;
                timerText.text = "0:00";
                Debug.Log("타이머 0 도달 → 타임아웃 처리 호출!");
                OnTimeLimitReached();
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
        Debug.Log(">>> StartTimer 호출됨");
        timer = timeLimit;
        isTimerRunning = true;
        timerText.gameObject.SetActive(true);
        UpdateTimerText();
    }

    public void StopTimer()
    {
        Debug.Log(">>> StopTimer 호출됨");
        isTimerRunning = false;
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);
        timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }

    private IEnumerator ClosePanelAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        targetPanel.SetActive(false);
        if (timeOutText != null)
        {
            timeOutText.gameObject.SetActive(false);
        }
        Debug.Log(">>> 패널 닫힘 완료, 타임아웃 상태: " + wasTimedOut);
    }

    void OnTimeLimitReached()
    {
        Debug.Log(">>> OnTimeLimitReached 호출됨");

        wasTimedOut = true;
        Debug.Log(">>> wasTimedOut 값: " + wasTimedOut);

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
        Debug.Log(">>> OpenPanel 호출됨, 이전 wasTimedOut: " + wasTimedOut);

        targetPanel.SetActive(true);

        // 강제 리셋
        wasTimedOut = false;
        timer = timeLimit;          // 타이머 리셋
        isTimerRunning = true;      // 타이머 다시 시작!

        timerText.gameObject.SetActive(true);
        UpdateTimerText();

        if (timeOutText != null)
        {
            timeOutText.gameObject.SetActive(false);
        }

        Debug.Log(">>> OpenPanel 끝, 타이머 리셋됨: " + timer + ", isTimerRunning: " + isTimerRunning);
    }


}