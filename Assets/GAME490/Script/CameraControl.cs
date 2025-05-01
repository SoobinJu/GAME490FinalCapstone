using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraControl : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (Camera.main != null && Camera.main.orthographic)
        {
            if (scene.name == "Game3" || scene.name == "Red_Green")
            {
                Camera.main.orthographicSize = 20f; // ¡‹æ∆øÙ
            }
            else
            {
                Camera.main.orthographicSize = 9f; // ±◊ ø‹ æ¿: ¡‹¿Œ
            }
        }
    }
}
