using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlayy : MonoBehaviour
{
    public GameObject panel;

    // Start is called before the first frame update
    void Start()
    {
        // Hide everything at the start
        panel.SetActive(true);
    }

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
    }
}
