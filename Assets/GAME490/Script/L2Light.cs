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

    AudioSource audioSource;
    public AudioClip lightSound;

    public Text lightCounter;

    private int useCount = 0;
    public static int maxUses = 2;
    private bool ableE = true;
    private int remained;

    private void Start()
    {
        remained = maxUses - useCount;

        audioSource = gameObject.AddComponent<AudioSource>();

        if (lightImage != null)
        {
            lightImage.sprite = ableImage;
        }

        UpdateGUI();

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
            remained = maxUses - useCount;
            StartCoroutine(TurnOn());
            UpdateGUI();

            if (useCount == maxUses && lightImage != null)
            {
                lightImage.sprite = unableImage;
            }

        }    
        else
        {
            Debug.Log("You used all lights.");
        }

    }

    private IEnumerator TurnOn()
    {
        audioSource.PlayOneShot(lightSound);
        ableE = false;
        lightEffect.SetActive(false);
        yield return new WaitForSeconds(8f);
        lightEffect.SetActive(true);
        ableE = true;
    }

    public void UpdateGUI()
    {
        lightCounter.text = remained.ToString();
    }
}