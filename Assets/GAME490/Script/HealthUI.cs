using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    GameObject[] Hearts;
    public Text Scoreui;
    public Sprite normalheart;
    public Sprite abnormalheart;
    GameObject Heart1;
    GameObject Heart2;
    GameObject Heart3;

    void Start()
    {
        //Hearts = GameObject.FindGameObjectsWithTag("HeartUI");
        Heart1 = GameObject.Find("Heart1");
        Heart2 = GameObject.Find("Heart2");
        Heart3 = GameObject.Find("Heart3");
        Hearts = new GameObject[] { Heart1, Heart2, Heart3 };
        Debug.Log(Hearts.Length);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void updatehealthUI(int health)
    {
        for (int i = 0; i < Hearts.Length; i++)
        {
            if (i < (health - 1))
            {
                Image image = Hearts[i].GetComponent<Image>();
                image.sprite = normalheart;
            }
            else
            {
                Image image = Hearts[i].GetComponent<Image>();
                image.sprite = abnormalheart;
            }

        }
    }

    public void updatecore(int score)
    {
        Scoreui.text = "Treasures : " + score;
    }

}
