using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;
using UnityEngine.UI;

public class L2Time : MonoBehaviour
{
    public float timeLimit = 4f;
    public float currentTime;
    public PlayerHealth healthScript;
    private bool isTimeOver = false;
    public bool isTimerRunning = true;

    public Text time;

    public GameObject howToplay;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("SavedTimeLimit"))
        {
            currentTime = PlayerPrefs.GetFloat("SavedTimeLimit");
            PlayerPrefs.DeleteKey("SavedTimeLimit");
        }
        else
        {
            currentTime = timeLimit;
        }    
    }

    // Update is called once per frame
    void Update()
    {
        if (howToplay.activeSelf)
        {
            Time.timeScale = 0f;
        }

        if (!isTimerRunning) return;

        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            time.text = "Remained time: " + Mathf.Ceil(currentTime).ToString();
        }
        else if (!isTimeOver)
        {
            time.text = "Time Over";
            TimeOver();
            isTimeOver = true;
        }
    }

    private void TimeOver()
    {
        healthScript.Death();
    }

    public void L2TimePause()
    {
        isTimerRunning = false;
    }
}
