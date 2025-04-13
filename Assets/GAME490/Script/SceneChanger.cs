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
        // 2√  ¥Î±‚
        yield return new WaitForSeconds(0.5f);

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
