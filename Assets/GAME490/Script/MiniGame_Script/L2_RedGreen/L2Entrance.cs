using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class L2Entrance : MonoBehaviour
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
            PlayerPrefs.SetInt("ReturningFromBuilding", 1);
            PlayerPrefs.SetString("LastEnteredTrigger", "A");
            PlayerPrefs.Save();

            Debug.Log("Saved Player position and time");
            alarmPanel.SetActive(true);

            StartCoroutine(SceneLoad(collision));
        }
    }

    IEnumerator SceneLoad(Collider2D collision)
    {
        yield return new WaitForSeconds(3f);
        collision.GetComponent<Inventory>()?.SaveInventory();
        SceneManager.LoadScene("Red_Green");
    }
}
