using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RicecakeMiniGame : MonoBehaviour
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
        else if (gameEnded)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                SceneManager.LoadScene("Game1");
            }
        }
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
            resultText.text = "Congratulations! Deokchil, you made the chewiest and most delicious Rice cake, Tteok in the entire village!  \r\n\r\nThanks to this, you've become known as not only smart, but also someone who’s amazing at making Reice cake, Tteok! " +
                "\r\n\r\nAs a reward,+50 HP!!";
            PlayerPrefs.SetInt("RicecakeResult", 1);
        }
        else
        {
            resultText.text = "Oh no… unfortunately, you misjudged your strength while pounding the tteok, and it flew up into the sky… " +
                " \r\nToday’s village festival will have to go on without Rice cake, Tteok…";
            PlayerPrefs.SetInt("RicecakeResult", 0);
        }
    }
}
