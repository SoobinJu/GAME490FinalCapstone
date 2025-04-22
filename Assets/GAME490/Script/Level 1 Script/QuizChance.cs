using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class QuizChance : MonoBehaviour
{
    public TextMeshProUGUI chancesText;
    private int chances;

    // 타이머 연결 추가!
    public TimedPanelController timerController;

    void Start()
    {
        chances = PlayerPrefs.GetInt("Chances", 3);
        UpdateChancesText();
    }

    private void UpdateChancesText()
    {
        chancesText.text = chances + "/3";

        if (chances <= 0)
        {
            chancesText.color = Color.red;
        }
        else
        {
            chancesText.color = Color.white;
        }
    }

    public void StartDecreaseChances()
    {
        StartCoroutine(DecreaseChances());
    }

    private IEnumerator DecreaseChances()
    {
        chances--;
        PlayerPrefs.SetInt("Chances", chances);
        PlayerPrefs.Save();
        UpdateChancesText();
        Debug.Log("Wrong! Chances left: " + chances);

        // 기회가 다 떨어지면 타이머 멈추기!
        if (chances <= 0)
        {
            Debug.Log("Game Over! No chances left.");

            if (timerController != null)
            {
                timerController.StopTimer();  //  타이머 멈춤
            }

            Time.timeScale = 1f;
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("LoseScene");
        }
    }
}
