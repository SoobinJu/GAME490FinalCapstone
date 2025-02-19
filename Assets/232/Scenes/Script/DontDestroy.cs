using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    public bool IncludeChild = false;

    [HideInInspector]
    public string objectID;

    private void Awake()
    {
        //objectID = name + transform.position.ToString() + transform.eulerAngles.ToString();
        objectID = name;
    }

    // Start is called before the first frame update
    void Start()
    {
        Object.FindObjectsOfType<DontDestroy>();
        for (int i = 0; i < Object.FindObjectsOfType<DontDestroy>().Length; i++)
        {
            if (Object.FindObjectsOfType<DontDestroy>()[i] != this)
            {
                if (Object.FindObjectsOfType<DontDestroy>()[i].objectID == gameObject.name)
                {
                    Destroy(gameObject);
                }
            }

        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Scene curr = SceneManager.GetActiveScene();
        
        if (curr.name =="WinScene" || curr.name =="LoseScene")
        {
            Destroy(gameObject);
        }
    }

}