using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimedPanelController : MonoBehaviour
{
    public GameObject targetPanel;      // ������ �г�
    public Text timerText;              // ���� �ð� ǥ�ÿ� �ؽ�Ʈ
    public float timeLimit = 61f;       // ���� ���� �ð� (��: 1�� 30��)
    private float timer;
    private bool isTimerRunning = false;

    public QuizChance quizChance;  // ��ȸ ���� ��ũ��Ʈ ����
    public Text timeOutText;  //  Time Out! �ؽ�Ʈ ����

    private bool wasTimedOut = false;  // Ÿ�Ӿƿ� ���� ����


    public AudioClip timeUpSound;
    public AudioSource audioSource;

    void Start()
    {
        StartTimer();  // �ϴ� �׽�Ʈ������ �ڵ� ���۵ǰ� �غ���!
    }


    void Update()
    {
        Debug.Log(">>> Update() �۵���, Ÿ�̸� ����: " + timer + ", isTimerRunning: " + isTimerRunning);

        if (isTimerRunning)
        {
            timer -= Time.unscaledDeltaTime;

            if (timer <= 0.01f)
            {
                timer = 0;
                isTimerRunning = false;
                timerText.text = "0:00";
                Debug.Log("Ÿ�̸� 0 ���� �� Ÿ�Ӿƿ� ó�� ȣ��!");
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





    // �г� ���� �� Ÿ�̸� ����
    public void StartTimer()
    {
        Debug.Log(">>> StartTimer ȣ���");
        timer = timeLimit;
        isTimerRunning = true;
        timerText.gameObject.SetActive(true);
        UpdateTimerText();
    }

    public void StopTimer()
    {
        Debug.Log(">>> StopTimer ȣ���");
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
        Debug.Log(">>> �г� ���� �Ϸ�, Ÿ�Ӿƿ� ����: " + wasTimedOut);
    }

    void OnTimeLimitReached()
    {
        Debug.Log(">>> OnTimeLimitReached ȣ���");

        wasTimedOut = true;
        Debug.Log(">>> wasTimedOut ��: " + wasTimedOut);

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
        Debug.Log(">>> OpenPanel ȣ���, ���� wasTimedOut: " + wasTimedOut);

        targetPanel.SetActive(true);

        // ���� ����
        wasTimedOut = false;
        timer = timeLimit;          // Ÿ�̸� ����
        isTimerRunning = true;      // Ÿ�̸� �ٽ� ����!

        timerText.gameObject.SetActive(true);
        UpdateTimerText();

        if (timeOutText != null)
        {
            timeOutText.gameObject.SetActive(false);
        }

        Debug.Log(">>> OpenPanel ��, Ÿ�̸� ���µ�: " + timer + ", isTimerRunning: " + isTimerRunning);
    }


}