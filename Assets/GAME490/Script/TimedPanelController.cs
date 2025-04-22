using UnityEngine;
using UnityEngine.UI;

public class TimedPanelController : MonoBehaviour
{
    public GameObject targetPanel;      // 관리할 패널
    public Text timerText;              // 남은 시간 표시용 텍스트
    public float timeLimit = 90f;       // 공통 제한 시간 (예: 1분 30초)
    private float timer;
    private bool isTimerRunning = false;

    public AudioClip timeUpSound;
    public AudioSource audioSource;

    void Update()
    {
        if (isTimerRunning)
        {
            // Time.unscaledDeltaTime 사용 일시정지 상태에서도 타이머 작동!
            timer -= Time.unscaledDeltaTime;

            // 분:초 형식으로 텍스트 표시
            int minutes = Mathf.FloorToInt(timer / 60f);
            int seconds = Mathf.FloorToInt(timer % 60f);
            timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);

            if (timer <= 0)
            {
                timer = 0;
                isTimerRunning = false;
                timerText.text = "0:00";
                OnTimeLimitReached();
            }
        }
    }

    // 패널 열릴 때 타이머 시작
    public void StartTimer()
    {
        timer = timeLimit;
        isTimerRunning = true;
        timerText.gameObject.SetActive(true);

        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);
        timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }

    // 패널 닫힐 때 타이머 정지 (텍스트는 그대로)
    public void StopTimer()
    {
        isTimerRunning = false;
        // timerText.gameObject.SetActive(false); //  텍스트 숨기지 않음
    }

    void OnTimeLimitReached()
    {
        Debug.Log("Time's up!");

        if (timeUpSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(timeUpSound);
        }

        targetPanel.SetActive(false);           // 패널 닫기
        // timerText.gameObject.SetActive(false); //  텍스트 숨기지 않음
    }
}
