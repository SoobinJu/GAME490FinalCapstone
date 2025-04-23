using UnityEngine;

public class SequentialPanelTrigger : MonoBehaviour
{
    public string clueID; // 👈 Set this in the Inspector

    public GameObject panelA;
    public GameObject panelB;
    private int interactionStage = 0; // 0: ¾Æ¹«°Íµµ ¾È ¿­¸², 1: A ¿­¸², 2: B ¿­¸²

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
            case 0: // ÆÐ³Î A ¿­±â
                panelA.SetActive(true);
                Time.timeScale = 0f;
                interactionStage = 1;

                if (!PlayerPrefs.HasKey(clueID))
                {
                    PlayerPrefs.SetInt(clueID, 1); // 🔐 Save that this clue was found
                    GameProgressTracker.Instance.FoundClue();
                    Debug.Log("✅ Clue '" + clueID + "' collected!");
                }
                else
                {
                    Debug.Log("❗Clue '" + clueID + "' was already collected.");
                }


                // ÄûÁî°¡ ¿Ï·áµÇ¾úÀ¸¸é resultText ´Ù½Ã º¸¿©ÁÖ±â
                if (quizManager != null && quizManager.resultText != null && quizManager.IsQuizCompleted())
                {
                    quizManager.resultText.gameObject.SetActive(true);
                }

                break;

            case 1: // A ´Ý°í B ¿­±â
                panelA.SetActive(false);
                panelB.SetActive(true);
                Time.timeScale = 0f;  // <<< ¿©±â Ãß°¡µÊ!
                interactionStage = 2;
                break;

            case 2: // B ´Ý±â
                panelB.SetActive(false);
                Time.timeScale = 1f;
                interactionStage = 0;

                // ´ÝÈú ¶§´Â resultText ²¨ÁÖ±â
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
            quizManager.resultText.gameObject.SetActive(false); // ¹ÛÀ¸·Î ³ª°¡µµ ¼û±â±â
        }
    }
}
