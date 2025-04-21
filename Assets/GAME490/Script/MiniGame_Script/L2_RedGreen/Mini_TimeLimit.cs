using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;
using UnityEngine.UI;

public class Mini_TimeLimit : MonoBehaviour
{
    public float timeLimit = 4f;
    private float currentTime;
    public PlayerHealth healthScript;
    
    private bool isTimeOver = false;
    public bool isTimerRunning = true;

    public Text time;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = timeLimit;
    }

    // Update is called once per frame
    void Update()
    {
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

    public void TimePause()
    {
        isTimerRunning = false;
    }
}
