using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    [SerializeField] GameObject panel;

    [SerializeField] Image timeImage;

    [SerializeField] Text timeText;

    [SerializeField] float duration, currentTime;

    public GameObject SettingsButton;
    public GameObject MainmenuButton;
    public GameObject RestartButton;

    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);
        currentTime = duration;
        timeText.text = currentTime.ToString();
        StartCoroutine(TimeIEn());

    }
    IEnumerator TimeIEn()
    {
        while(currentTime >= 0)
        {
            timeImage.fillAmount = Mathf.InverseLerp(0, duration, currentTime);
            timeText.text = currentTime.ToString();
            yield return new WaitForSeconds(1f);
            currentTime--;
        }
        OpenPanel();
    }

    void OpenPanel()
    {
        timeText.text = "";
        panel.SetActive(true);
        SettingsButton.SetActive(false);
        MainmenuButton.SetActive(true);
        RestartButton.SetActive(true);
        PauseGame();
    }


    private void PauseGame()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
