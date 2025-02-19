using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string ScenetoGo;

    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GoScene()
    {
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
