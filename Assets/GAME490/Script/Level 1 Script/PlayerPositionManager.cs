﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using static UnityEngine.Rendering.DebugUI;

public class PlayerPositionManager : MonoBehaviour
{
    private Transform defaultSpawnPoint; // Default spawn point in original scene
    private Transform buildingSpawnPoint; // Spawn point inside buildings

    public GameObject HTPpanel;

    private void Start()
    {
        Debug.Log(" PlayerPositionManager Started in Scene: " + SceneManager.GetActiveScene().name);

        // Find the correct spawn point
        if (SceneManager.GetActiveScene().name == "Game1")
        {
            defaultSpawnPoint = GameObject.Find("SpawnPoint_Default")?.transform;
        }
        else // In building scenes, find the building spawn point
        {
            buildingSpawnPoint = GameObject.FindWithTag("BuildingSpawnPoint")?.transform;
        }

        SetPlayerPositionAfterLoad(); // Force update AFTER scene fully loads
    }

    private void SetPlayerPositionAfterLoad()
    {
        //yield return new WaitForSeconds(0.1f); // 이거 있으니까 딜레이 생겨서 지움

        // If returning to original scene, move to saved position
        if (SceneManager.GetActiveScene().name == "Game1" && PlayerPrefs.GetInt("ReturningFromBuilding", 0) == 1)
        {
            float x = PlayerPrefs.GetFloat("LastExitX", 0);
            float y = PlayerPrefs.GetFloat("LastExitY", 0);
            Debug.Log(" Returning to Saved Position in Original Scene: X=" + x + " Y=" + y);
            transform.position = new Vector3(x, y, transform.position.z);
            PlayerPrefs.SetInt("ReturningFromBuilding", 0); // Reset flag
            PlayerPrefs.Save();
            HTPpanel.SetActive(false);
        }
        // If entering a building, use building spawn point
        else if (buildingSpawnPoint != null)
        {
            Debug.Log("Entering Building. Moving Player to: " + buildingSpawnPoint.position);
            transform.position = buildingSpawnPoint.position;
        }
        // If no saved position, spawn at default
        else if (defaultSpawnPoint != null)
        {
            Debug.Log("Spawning at Default Spawn Point");
            transform.position = defaultSpawnPoint.position;
        }
        else
        {
            Debug.Log("No Spawn Point Found! Setting Safe Position.");
            transform.position = new Vector3(3, 10, 0); // Set to a safe default position
        }
    }

}
