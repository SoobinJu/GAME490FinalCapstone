using UnityEngine;

public class InteractionHandler : MonoBehaviour
{

    public string clueID; // Set this in the Inspector for each clue!

    public GameObject panel;
    private bool canOpen = false;
    private bool panelOpen = false;
    private bool clueFounded = false;
    private bool answerCorrect = false; // Á¤´ä ¸ÂÃè´ÂÁö È®ÀÎ¿ë

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

        // Å¸ÀÌ¸Ó ÄÁÆ®·Ñ·¯ ¿¬°á
        timerController = panel.GetComponent<TimedPanelController>();
        if (timerController == null)
        {
            Debug.LogError(">>> TimedPanelController ¸ø Ã£À½!");
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

        // Á¤´ä ¸ÂÃèÀ¸¸é Å¸ÀÌ¸Ó ÀÛµ¿ ¾È ÇÔ
        if (!answerCorrect && timerController != null)
        {
            timerController.OpenPanel(); // Å¸ÀÌ¸Ó ¸®¼Â + ÀÛµ¿
        }

        if (!PlayerPrefs.HasKey(clueID))
        {
            PlayerPrefs.SetInt(clueID, 1);
            GameProgressTracker.Instance.FoundClue();
            Debug.Log("✅ Clue '" + clueID + "' collected from InteractionHandler!");
        }
        else
        {
            Debug.Log("❗Clue '" + clueID + "' was already collected.");
        }

    }

    public void ClosePanel()
    {
        panel.SetActive(false);
        panelOpen = false;
        UpdateGamePauseState();

        // Á¤´ä ¸ÂÃèÀ¸¸é ¸ØÃßÁö ¸»°í ±×´ë·Î µÎ±â
        if (!answerCorrect && timerController != null)
        {
            timerController.StopTimer();
        }
    }

    private void UpdateGamePauseState()
    {
        Time.timeScale = panel.activeSelf ? 0f : 1f;
    }

    // Á¤´ä ¸ÂÃèÀ» ¶§ ²À È£ÃâÇØÁà!
    public void OnAnswerCorrect()
    {
        Debug.Log("Á¤´ä ¸ÂÃã ¡æ Å¸ÀÌ¸Ó ¸ØÃã + »óÅÂ ÀúÀå!");
        answerCorrect = true;

        if (timerController != null)
        {
            timerController.StopTimer(); // Å¸ÀÌ¸Ó ¸ØÃã
        }
    }
}
