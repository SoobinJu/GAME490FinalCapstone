using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    public GameObject panel;
    private bool canOpen = false;
    private bool panelOpen = false;
    private bool clueFounded = false;
    private bool answerCorrect = false; // 정답 맞췄는지 확인용

    private SpriteRenderer spriteRenderer;
    public Sprite defaultImage;
    public Sprite changedImage;

    AudioSource audioSource;
    public AudioClip PanelSound;

    private TimedPanelController timerController;

    private void Start()
    {
        panel.SetActive(false);

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = defaultImage;

        audioSource = gameObject.AddComponent<AudioSource>();

        // 타이머 컨트롤러 연결
        timerController = panel.GetComponent<TimedPanelController>();
        if (timerController == null)
        {
            Debug.LogError(">>> TimedPanelController 못 찾음!");
        }
    }

    private void Update()
    {
        if (canOpen && Input.GetKeyDown(KeyCode.E))
        {
            if (panelOpen)
            {
                ClosePanel();
                audioSource.PlayOneShot(PanelSound);
            }
            else
            {
                OpenPanel();
                audioSource.PlayOneShot(PanelSound);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canOpen = true;
            spriteRenderer.sprite = changedImage;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canOpen = false;
            ClosePanel();
            spriteRenderer.sprite = defaultImage;
        }
    }

    public void OpenPanel()
    {
        panel.SetActive(true);
        panelOpen = true;
        UpdateGamePauseState();

        // 정답 맞췄으면 타이머 작동 안 함
        if (!answerCorrect && timerController != null)
        {
            timerController.OpenPanel(); // 타이머 리셋 + 작동
        }

        if (!clueFounded)
        {
            clueFounded = true;
            GameProgressTracker tracker = FindObjectOfType<GameProgressTracker>();

            if (tracker != null)
            {
                tracker.FoundClue();
                Debug.Log("Clue Found! Progress updated.");
            }
            else
            {
                Debug.LogError("GameProgressTracker not found!");
            }
        }
    }

    public void ClosePanel()
    {
        panel.SetActive(false);
        panelOpen = false;
        UpdateGamePauseState();

        // 정답 맞췄으면 멈추지 말고 그대로 두기
        if (!answerCorrect && timerController != null)
        {
            timerController.StopTimer();
        }
    }

    private void UpdateGamePauseState()
    {
        Time.timeScale = panel.activeSelf ? 0f : 1f;
    }

    // 정답 맞췄을 때 꼭 호출해줘!
    public void OnAnswerCorrect()
    {
        Debug.Log("정답 맞춤 → 타이머 멈춤 + 상태 저장!");
        answerCorrect = true;

        if (timerController != null)
        {
            timerController.StopTimer(); // 타이머 멈춤
        }
    }
}
