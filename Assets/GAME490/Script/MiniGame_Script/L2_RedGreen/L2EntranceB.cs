

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class L2EntranceB : MonoBehaviour
{
    public L2Time timeScript;
    public GameObject alarmPanel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerPrefs.SetFloat("SavedTimeLimit", timeScript.currentTime);

            PlayerPrefs.SetFloat("LastExitX", collision.transform.position.x);
            PlayerPrefs.SetFloat("LastExitY", collision.transform.position.y);
            PlayerPrefs.SetInt("ReturningFromMini", 1);
            PlayerPrefs.SetString("LastEnteredTrigger", "B");

            PlayerPrefs.SetFloat("PlayerHealth", FindObjectOfType<PlayerHealth>().GetHealth());
            PlayerPrefs.SetInt("ReturningFromMiniGame", 1);

            collision.GetComponent<Inventory>()?.SaveInventory();
            PlayerPrefs.Save();

            Debug.Log("Saved Player position and time");
            alarmPanel.SetActive(true);

            StartCoroutine(SceneLoad());
        }
    }

    IEnumerator SceneLoad()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Red_Green");
    }
}
