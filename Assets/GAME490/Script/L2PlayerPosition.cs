using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class L2PlayerPosition : MonoBehaviour
{
    private Transform defaultSpawnPoint;
    private Transform miniSpawnPoint;

    public GameObject playPanel;

    private void Start()
    {
        Debug.Log(" PlayerPositionManager Started in Scene: " + SceneManager.GetActiveScene().name);

        if (SceneManager.GetActiveScene().name == "Game3")
        {
            defaultSpawnPoint = GameObject.Find("SpawnPoint_Default")?.transform;
        }
        else
        {
            miniSpawnPoint = GameObject.FindWithTag("MiniSpawnPoint")?.transform;
        }

        SetPlayerPositionAfterLoad();
    }

    private void SetPlayerPositionAfterLoad()
    {

        if (SceneManager.GetActiveScene().name == "Game3" && PlayerPrefs.GetInt("ReturningFromBuilding", 0) == 1)
        {
            float x = PlayerPrefs.GetFloat("LastExitX", 0);
            float y = PlayerPrefs.GetFloat("LastExitY", 0);
            Debug.Log(" Returning to Saved Position in Original Scene: X=" + x + " Y=" + y);
            transform.position = new Vector3(x, y, transform.position.z);
            PlayerPrefs.SetInt("ReturningFromBuilding", 0);
            PlayerPrefs.Save();

            playPanel.SetActive(false);
        }

        else if (miniSpawnPoint != null)
        {
            Debug.Log("Entering Mini game. " + miniSpawnPoint.position);
            transform.position = miniSpawnPoint.position;
        }

        else if (defaultSpawnPoint != null)
        {
            Debug.Log("Spawning at Default Spawn Point");
            transform.position = defaultSpawnPoint.position;
        }
        else
        {
            Debug.Log("No Spawn Point Found! Setting Safe Position.");
            transform.position = new Vector3(3, 10, 0);
        }
    }
}
