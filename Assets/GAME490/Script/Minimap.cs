using UnityEngine;
using UnityEngine.SceneManagement;

public class Minimap : MonoBehaviour
{
    public GameObject minimapCanvas;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject); // �� �Ѱܵ� ��Ƴ���
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
                minimapCanvas.SetActive(false); // �̴ϸ� ��Ȱ��ȭ
            }
        }
        else
        {
            if (minimapCanvas != null)
            {
                minimapCanvas.SetActive(true); // �̴ϸ� Ȱ��ȭ
            }
        }
    }
}
