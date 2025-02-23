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

    // Start is called before the first frame update
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

        if (chances <= 0)
        {
            Debug.Log("Game Over! No chances left.");
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene("LoseScene");
        }
    }
}
