using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPositionManager : MonoBehaviour
{
    private Transform defaultSpawnPoint; // Default spawn point in original scene
    private Transform buildingSpawnPoint; // Spawn point inside buildings

    private void Start()
    {
        Debug.Log("🟢 PlayerPositionManager Started in Scene: " + SceneManager.GetActiveScene().name);

        // Try to find the default spawn point (ONLY in Original Scene)
        if (SceneManager.GetActiveScene().name == "Game1") // Replace with your actual original scene name
        {
            defaultSpawnPoint = GameObject.Find("SpawnPoint_Default")?.transform;
            if (defaultSpawnPoint != null)
                Debug.Log("✅ Found Default Spawn Point at: " + defaultSpawnPoint.position);
            else
                Debug.Log("❌ Default Spawn Point NOT Found! Check Hierarchy.");
        }
        else // Try to find the building spawn point in building scenes
        {
            buildingSpawnPoint = GameObject.FindWithTag("BuildingSpawnPoint")?.transform;
            if (buildingSpawnPoint != null)
                Debug.Log("✅ Found Building Spawn Point at: " + buildingSpawnPoint.position);
            else
                Debug.Log("❌ Building Spawn Point NOT Found! Check Tags and Names!");
        }

        // If returning to the original scene, load the saved exit position
        if (SceneManager.GetActiveScene().name == "Game1" && PlayerPrefs.GetInt("ReturningFromBuilding", 0) == 1)
        {
            float x = PlayerPrefs.GetFloat("LastExitX", transform.position.x);
            float y = PlayerPrefs.GetFloat("LastExitY", transform.position.y);
            Debug.Log("🎯 Returning to Saved Position in Original Scene: X=" + x + " Y=" + y);
            transform.position = new Vector3(x, y, transform.position.z);
            PlayerPrefs.SetInt("ReturningFromBuilding", 0);
            PlayerPrefs.Save();
        }
        else if (buildingSpawnPoint != null)
        {
            Debug.Log("🏗 Entering Building. Spawning at Building Spawn Point: " + buildingSpawnPoint.position);
            transform.position = buildingSpawnPoint.position;
        }
        else if (defaultSpawnPoint != null)
        {
            Debug.Log("🌍 Spawning at Default Spawn Point");
            transform.position = defaultSpawnPoint.position;
        }
        else
        {
            Debug.Log("⚠️ NO SPAWN POINT FOUND! Moving Player to Emergency Spawn.");
            transform.position = new Vector3(5, 5, 0); // Change to a safe fallback location
        }
    }




}
