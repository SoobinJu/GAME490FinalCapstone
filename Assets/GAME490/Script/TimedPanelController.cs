using UnityEngine;
using UnityEngine.UI;

public class TimedPanelController : MonoBehaviour
{
    public GameObject targetPanel;      // ������ �г�
    public Text timerText;              // ���� �ð� ǥ�ÿ� �ؽ�Ʈ
    public float timeLimit = 90f;       // ���� ���� �ð� (��: 1�� 30��)
    private float timer;
    private bool isTimerRunning = false;

    public AudioClip timeUpSound;
    public AudioSource audioSource;

    void Update()
    {
        if (isTimerRunning)
        {
            // Time.unscaledDeltaTime ��� �Ͻ����� ���¿����� Ÿ�̸� �۵�!
            timer -= Time.unscaledDeltaTime;

            // ��:�� �������� �ؽ�Ʈ ǥ��
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

    // �г� ���� �� Ÿ�̸� ����
    public void StartTimer()
    {
        timer = timeLimit;
        isTimerRunning = true;
        timerText.gameObject.SetActive(true);

        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);
        timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }

    // �г� ���� �� Ÿ�̸� ���� (�ؽ�Ʈ�� �״��)
    public void StopTimer()
    {
        isTimerRunning = false;
        // timerText.gameObject.SetActive(false); //  �ؽ�Ʈ ������ ����
    }

    void OnTimeLimitReached()
    {
        Debug.Log("Time's up!");

        if (timeUpSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(timeUpSound);
        }

        targetPanel.SetActive(false);           // �г� �ݱ�
        // timerText.gameObject.SetActive(false); //  �ؽ�Ʈ ������ ����
    }
}
