using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string ScenetoGo;
    public GameObject target;

    public void GoScene()
    {

        StartCoroutine(GoSceneCoroutine());
    }

    private IEnumerator GoSceneCoroutine()
    {
        // 2초 대기
        yield return new WaitForSeconds(0.5f);

        if (ScenetoGo == "Game3")
        {
            PlayerPrefs.DeleteKey("SwordCount");
            PlayerPrefs.DeleteKey("AmuletCount");
            PlayerPrefs.DeleteKey("LightCount");

            // Collectible ID 별로 지운 것들
            PlayerPrefs.DeleteKey("Amulet1");
            PlayerPrefs.DeleteKey("Amulet2");
            PlayerPrefs.DeleteKey("Sword1");
            PlayerPrefs.DeleteKey("Sword2");

            PlayerPrefs.DeleteKey("LastEnteredTrigger");
            PlayerPrefs.DeleteKey("SavedTimeLimit");
            PlayerPrefs.DeleteKey("ReturningFromMini");

            PlayerPrefs.Save();
            Debug.Log("Level 2 data was deleted");
        }

        Debug.Log("Going to: " + ScenetoGo);
        SceneManager.LoadScene(ScenetoGo);
    }

    public void changeactive(bool active)
    {
        if(active == true)
        {
            target.SetActive(true);
        }
        if (active != false)
        {
            target.SetActive(false);
        }
    }

}
