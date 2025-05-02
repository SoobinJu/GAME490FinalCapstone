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
    public GameObject howToPanel;

    AudioSource audioSource;
    public AudioClip lightSound;

    public Text lightCounter;

    private int useCount = 0;
    public static int maxUses = 2;
    private bool ableE = true;
    public int remained;

    private void Start()
    {
        LoadInventory();

        useCount = Mathf.Clamp(maxUses - remained, 0, maxUses);

        audioSource = gameObject.AddComponent<AudioSource>();

        if (lightImage != null)
        {
            lightImage.sprite = ableImage;
        }

        UpdateGUI();

    }

    private void Update()
    {
        if (ableE && howToPanel != null && !howToPanel.activeSelf &&Input.GetKeyDown(KeyCode.E))
        {
            DelayLight();
        }
    }

    public void DelayLight()
    {
        if (remained > 0)
        {
            remained--;
            StartCoroutine(TurnOn());
            UpdateGUI();

            if (remained == 0 && lightImage != null)
            {
                lightImage.sprite = unableImage;
            }

            SaveInventory();
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
        lightEffect.SetActive(true);
        yield return new WaitForSeconds(8f);
        lightEffect.SetActive(false);
        ableE = true;
    }

    public void UpdateGUI()
    {
        lightCounter.text = remained.ToString();
    }

    public void SaveInventory()
    {
        PlayerPrefs.SetInt("LightCount", remained);
        PlayerPrefs.Save();
    }

    private void LoadInventory()
    {
        if (PlayerPrefs.HasKey("LightCount"))
        {
            remained = PlayerPrefs.GetInt("LightCount");
        }
        else
        {
            remained = maxUses;
        }

        UpdateGUI();
    }
}