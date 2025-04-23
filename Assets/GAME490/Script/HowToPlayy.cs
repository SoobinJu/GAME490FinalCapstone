using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlayy : MonoBehaviour
{
    public GameObject panel;

    void Start()
    {
        // 💡 Skip How to Play panel if returning from mini-game
        if (PlayerPrefs.HasKey("MinigameReturnX"))
        {
            panel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ClosePanel();
        }
    }

    public void ClosePanel()
    {
        panel.SetActive(false);
        Time.timeScale = 1f;
    }
}
