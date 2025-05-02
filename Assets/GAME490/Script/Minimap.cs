using UnityEngine;
using UnityEngine.SceneManagement;

public class Minimap : MonoBehaviour
{
    public GameObject minimapCanvas;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject); // 씬 넘겨도 살아남게
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string[] scenesToHideMinimap = { "Minigame", "Minigame2", "Game3", "Red_Green", "Narration2" ,"Narration3" };

        if (System.Array.Exists(scenesToHideMinimap, name => name == scene.name))
        {
            if (minimapCanvas != null)
            {
                minimapCanvas.SetActive(false); // 미니맵 비활성화
            }
        }
        else
        {
            if (minimapCanvas != null)
            {
                minimapCanvas.SetActive(true); // 미니맵 활성화
            }
        }
    }
}
