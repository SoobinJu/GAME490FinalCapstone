using UnityEngine;

public class SequentialPanelTrigger : MonoBehaviour
{
    public GameObject panelA;
    public GameObject panelB;
    private int interactionStage = 0; // 0: 아무것도 안 열림, 1: A 열림, 2: B 열림

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
            case 0: // 패널 A 열기
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

                // 퀴즈가 완료되었으면 resultText 다시 보여주기
                if (quizManager != null && quizManager.resultText != null && quizManager.IsQuizCompleted())
                {
                    quizManager.resultText.gameObject.SetActive(true);
                }

                break;

            case 1: // A 닫고 B 열기
                panelA.SetActive(false);
                panelB.SetActive(true);
                interactionStage = 2;
                break;

            case 2: // B 닫기
                panelB.SetActive(false);
                Time.timeScale = 1f;
                interactionStage = 0;

                // 닫힐 때는 resultText 꺼주기
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
            quizManager.resultText.gameObject.SetActive(false); // 밖으로 나가도 숨기기
        }
    }
}
