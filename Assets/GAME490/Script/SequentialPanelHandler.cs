using UnityEngine;

public class SequentialPanelTrigger : MonoBehaviour
{
    public string clueID; // Set this in the Inspector

    public GameObject panelA;
    public GameObject panelB;
    private int interactionStage = 0;

    private bool canInteract = false;

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

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
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
            case 0: // Panel A 열기
                panelA.SetActive(true);
                Time.timeScale = 0f;
                interactionStage = 1;

                if (!PlayerPrefs.HasKey(clueID))
                {
                    PlayerPrefs.SetInt(clueID, 1);
                    GameProgressTracker.Instance.FoundClue();
                    Debug.Log("✅ Clue '" + clueID + "' collected!");
                }

                // 퀴즈가 끝났으면 결과만 보여주고
                if (quizManager != null && quizManager.IsQuizCompleted())
                {
                    quizManager.resultText.gameObject.SetActive(true);
                }
                else
                {
                    // 퀴즈 안끝났으면 다음 문제 강제로 보여줌
                    quizManager.ShowQuiz();  // 다음 문제 표시!
                }

                break;

            case 1: // Panel A 닫고 Panel B 열기
                panelA.SetActive(false);
                panelB.SetActive(true);
                Time.timeScale = 0f;
                interactionStage = 2;
                break;

            case 2: // Panel B 닫기
                panelB.SetActive(false);
                Time.timeScale = 1f;
                interactionStage = 0;

                // 패널 닫을 때 모든 UI 확실히 정리
                if (quizManager != null)
                {
                    if (quizManager.resultText != null)
                    {
                        quizManager.resultText.gameObject.SetActive(false);
                    }

                    if (quizManager.correctImage != null)
                    {
                        quizManager.correctImage.gameObject.SetActive(false);
                    }

                    if (quizManager.wrongImage != null)
                    {
                        quizManager.wrongImage.gameObject.SetActive(false);
                    }
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

        if (quizManager != null)
        {
            if (quizManager.resultText != null)
            {
                quizManager.resultText.gameObject.SetActive(false);
            }

            if (quizManager.correctImage != null)
            {
                quizManager.correctImage.gameObject.SetActive(false);
            }

            if (quizManager.wrongImage != null)
            {
                quizManager.wrongImage.gameObject.SetActive(false);
            }
        }
    }
}
