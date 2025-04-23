using UnityEngine;
using UnityEngine.SceneManagement;

public class RicecakeTrigger : MonoBehaviour
{
    public float minTimeBeforeTrigger = 60f; // 2분
    public float checkInterval = 5f; // 5초마다 한 번씩 체크
    public float triggerChance = 0.2f; // 20% 확률

    private float timeSinceStart = 0f;
    private float timeSinceLastCheck = 0f;
    private bool triggered = false;

    public AudioClip triggerSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (triggered) return;

        timeSinceStart += Time.deltaTime;
        timeSinceLastCheck += Time.deltaTime;

        if (timeSinceStart >= minTimeBeforeTrigger && timeSinceLastCheck >= checkInterval)
        {
            timeSinceLastCheck = 0f;

            if (Random.value < triggerChance)
            {
                TriggerMinigame();
            }
        }
    }

    void TriggerMinigame()
    {
        triggered = true;

        if (audioSource != null && triggerSound != null)
        {
            audioSource.PlayOneShot(triggerSound);
        }

        // 🧠 Save previous scene
        PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);

        // 💾 Save player position
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerPrefs.SetFloat("MinigameReturnX", player.transform.position.x);
            PlayerPrefs.SetFloat("MinigameReturnY", player.transform.position.y);
        }

        PlayerPrefs.SetFloat("MinigameReturnX", GameObject.FindWithTag("Player").transform.position.x);
        PlayerPrefs.SetFloat("MinigameReturnY", GameObject.FindWithTag("Player").transform.position.y);

        Invoke("GoToMinigame", 1f);
    }

    void GoToMinigame()
    {
        SceneManager.LoadScene("Minigame2");
    }
}
