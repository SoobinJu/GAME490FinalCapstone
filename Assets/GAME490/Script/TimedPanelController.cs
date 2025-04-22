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
        if (isTimerRunning)
        {
            timer -= Time.unscaledDeltaTime;

            if (timer <= 0.01f)  // �� ���� �ְ�, Ȯ���ϰ�!
            {
                timer = 0;
                isTimerRunning = false;
                timerText.text = "0:00";
                Debug.Log("Ÿ�̸� 0 ���� �� Ÿ�Ӿƿ� ó�� ȣ��!");
                OnTimeLimitReached();  // ������ ȣ���!
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
        Debug.Log("StartTimer ȣ��� �� Ÿ�̸� ���� �� �۵� ����!");

        timer = timeLimit;       // �ð� ���� (��: 61��)
        isTimerRunning = true;   // Ÿ�̸� �۵� �÷��� true
        timerText.gameObject.SetActive(true);  // �ؽ�Ʈ ���̰�

        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);
        timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);

        Debug.Log("StartTimer ���� �� timer = " + timer + ", isTimerRunning = " + isTimerRunning);
    }



    // �г� ���� �� Ÿ�̸� ���� (�ؽ�Ʈ�� �״��)
    public void StopTimer()
    {
        isTimerRunning = false;
        // timerText.gameObject.SetActive(false); //  �ؽ�Ʈ ������ ����
    }

    private IEnumerator ClosePanelAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);  //  �Ͻ����� �����ϰ� ��ٸ�

        targetPanel.SetActive(false);  // �г� �ݱ�

        if (timeOutText != null)
        {
            timeOutText.gameObject.SetActive(false);  // Time Out �ؽ�Ʈ �����
        }
    }


    void OnTimeLimitReached()
    {
        Debug.Log(">>> OnTimeLimitReached ȣ���!!!");  // �̰� ������ �߳�?

        wasTimedOut = true;
        Debug.Log(">>> wasTimedOut ��: " + wasTimedOut);  // true �������� Ȯ��

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
            StartTimer();      // Ÿ�̸� ���� ����
            wasTimedOut = false;  // ���� �ʱ�ȭ
        }
        else
        {
            timerText.gameObject.SetActive(true);  // ���� Ÿ�̸� ����
        }
    }






}