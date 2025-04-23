using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    public GameObject panel;
    private bool canOpen = false;
    private bool panelOpen = false;
    private bool clueFounded = false;
    private bool answerCorrect = false; // ���� ������� Ȯ�ο�

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

        // Ÿ�̸� ��Ʈ�ѷ� ����
        timerController = panel.GetComponent<TimedPanelController>();
        if (timerController == null)
        {
            Debug.LogError(">>> TimedPanelController �� ã��!");
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

        // ���� �������� Ÿ�̸� �۵� �� ��
        if (!answerCorrect && timerController != null)
        {
            timerController.OpenPanel(); // Ÿ�̸� ���� + �۵�
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

        // ���� �������� ������ ���� �״�� �α�
        if (!answerCorrect && timerController != null)
        {
            timerController.StopTimer();
        }
    }

    private void UpdateGamePauseState()
    {
        Time.timeScale = panel.activeSelf ? 0f : 1f;
    }

    // ���� ������ �� �� ȣ������!
    public void OnAnswerCorrect()
    {
        Debug.Log("���� ���� �� Ÿ�̸� ���� + ���� ����!");
        answerCorrect = true;

        if (timerController != null)
        {
            timerController.StopTimer(); // Ÿ�̸� ����
        }
    }
}
