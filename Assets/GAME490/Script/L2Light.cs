using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class L2Light : MonoBehaviour
{
    public GameObject lightEffect;

    public Image lightImage;
    public Sprite ableImage;
    public Sprite unableImage;

    private int useCount = 0;
    private int maxUses = 2;
    private bool ableE = true;

    private void Start()
    {
        if (lightImage != null)
        {
            lightImage.sprite = ableImage;
        }
    }

    private void Update()
    {
        if (ableE && Input.GetKeyDown(KeyCode.E))
        {
            DelayLight();
        }
    }

    public void DelayLight()
    {
        if (useCount < maxUses)
        {

            useCount++;
            StartCoroutine(TurnOn());

            if (useCount == maxUses && lightImage != null)
            {
                lightImage.sprite = unableImage;
            }

        }    
        else
        {
            Debug.Log("2°³ ´Ù ¾¸");
        }

    }

    private IEnumerator TurnOn()
    {
        ableE = false;
        lightEffect.SetActive(false);
        yield return new WaitForSeconds(8f);
        lightEffect.SetActive(true);
        ableE = true;
    }
}