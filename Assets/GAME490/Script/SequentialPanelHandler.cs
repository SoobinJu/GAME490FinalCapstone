using UnityEngine;

public class SequentialPanelTrigger : MonoBehaviour
{
    public GameObject panelA;
    public GameObject panelB;
    private int interactionStage = 0; // 0: �ƹ��͵� �� ����, 1: A ����, 2: B ����

    private bool canInteract = false;
    private bool clueFound = false;

    private SpriteRenderer spriteRenderer;
    public Sprite defaultImage;
    public Sprite changedImage;

    private AudioSource audioSource;
    public AudioClip panelSound;
    public QuizManager quizManager;

    void Start()
    {
        panelA.SetActive(false);
        panelB.SetActive(false);

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = defaultImage;

        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            HandleInteraction();
        }
    }

    private void HandleInteraction()
    {
        audioSource.PlayOneShot(panelSound);

        switch (interactionStage)
        {
            case 0: // �г� A ����
                panelA.SetActive(true);
                Time.timeScale = 0f;
                interactionStage = 1;

                if (!clueFound)
                {
                    clueFound = true;
                    GameProgressTracker tracker = FindObjectOfType<GameProgressTracker>();

                    if (tracker != null)
                    {
                        tracker.FoundClue();
                        Debug.Log("Clue Found! Progress updated.");
                    }
                    else
                    {
                        Debug.LogError("GameProgressTracker not found! Creating one...");
                        GameObject newTracker = new GameObject("GameProgressTracker");
                        tracker = newTracker.AddComponent<GameProgressTracker>();
                        tracker.FoundClue();
                    }
                }

                // ��� �Ϸ�Ǿ����� resultText �ٽ� �����ֱ�
                if (quizManager != null && quizManager.resultText != null && quizManager.IsQuizCompleted())
                {
                    quizManager.resultText.gameObject.SetActive(true);
                }

                break;

            case 1: // A �ݰ� B ����
                panelA.SetActive(false);
                panelB.SetActive(true);
                interactionStage = 2;
                break;

            case 2: // B �ݱ�
                panelB.SetActive(false);
                Time.timeScale = 1f;
                interactionStage = 0;

                // ���� ���� resultText ���ֱ�
                if (quizManager != null && quizManager.resultText != null)
                {
                    quizManager.resultText.gameObject.SetActive(false);
                }
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = true;
            spriteRenderer.sprite = changedImage;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = false;
            ResetPanels();
            spriteRenderer.sprite = defaultImage;
        }
    }

    private void ResetPanels()
    {
        panelA.SetActive(false);
        panelB.SetActive(false);
        interactionStage = 0;
        Time.timeScale = 1f;

        if (quizManager != null && quizManager.resultText != null)
        {
            quizManager.resultText.gameObject.SetActive(false); // ������ ������ �����
        }
    }
}
