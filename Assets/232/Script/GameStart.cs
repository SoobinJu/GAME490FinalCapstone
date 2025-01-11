using UnityEngine;

public class GameStart : MonoBehaviour
{
    public void StartGame()
    {
        SaveManager saveManager = FindObjectOfType<SaveManager>();
        saveManager.LoadGame();
    }
}
