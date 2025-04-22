using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlayy : MonoBehaviour
{
    public GameObject panel;

    // Update is called once per frame
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
