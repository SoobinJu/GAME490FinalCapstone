using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public GameObject targetPanel;

    public void TogglePanel()
    {
        targetPanel.SetActive(!targetPanel.activeSelf); // ƒ—¡Æ¿÷¿∏∏È ≤Ù∞Ì, ≤®¡Æ¿÷¿∏∏È ≈¥
    }

    public void OpenPanel()
    {
        targetPanel.SetActive(true);
    }

    public void ClosePanel()
    {
        targetPanel.SetActive(false);
    }
}
