using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GongjangMinigame : MonoBehaviour
{
    public GameObject introPanel;
    public GameObject gameUI;
    public GameObject resultPanel;

    public Image gaugeBar;
    public Text timerText;
    public Text resultText;

    public Image punishImage;
    public Sprite normalSprite;
    public Sprite hitSprite;

    private float gaugeValue = 0f;
    private float gaugeMax = 500f;
    private float timer = 40f;
    private bool isPlaying = false;
    private bool gameEnded = false;

    void Start()
    {
        introPanel.SetActive(true);
        gameUI.SetActive(false);
        resultPanel.SetActive(false);
    }

    void Update()
    {
        if (!isPlaying && !gameEnded)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartGame();
            }
        }
        else if (isPlaying)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                gaugeValue += 5f;
                gaugeValue = Mathf.Clamp(gaugeValue, 0f, gaugeMax);
                gaugeBar.fillAmount = gaugeValue / gaugeMax;
                punishImage.sprite = hitSprite;
            }
            if (Input.GetKeyUp(KeyCode.E))
            {
                punishImage.sprite = normalSprite;
            }

            timer -= Time.deltaTime;
            timerText.text = Mathf.Ceil(timer).ToString("00") + "s";

            if (timer <= 0f || gaugeValue >= gaugeMax)
            {
                EndGame();
            }
        }
        //else if (gameEnded)
        //{
        //    if (Input.GetKeyDown(KeyCode.E))
        //    {
        //        SceneManager.LoadScene("Game1");
        //    }
        //}
    }

    void StartGame()
    {
        introPanel.SetActive(false);
        gameUI.SetActive(true);
        isPlaying = true;
        timer = 20f;
        gaugeValue = 0f;
        gaugeBar.fillAmount = 0f;
    }

    void EndGame()
    {
        isPlaying = false;
        gameEnded = true;
        gameUI.SetActive(false);
        resultPanel.SetActive(true);

        if (gaugeValue >= gaugeMax)
        {
            resultText.text = "Congratulations! The criminal has reflected on their wrongdoing after the flogging. " +
                "\r\n\r\nAs a reward: +50 HP!";
            PlayerPrefs.SetInt("GongjangResult", 1);
        }
        else
        {
            resultText.text = "Unfortunately, the punishment wasn’t enough, and the criminal didn’t reflect on their wrongdoing... " +
                " \r\nIt seems likely that they’ll commit another crime in the future..";
            PlayerPrefs.SetInt("GongjangResult", 0);
        }
    }
}
