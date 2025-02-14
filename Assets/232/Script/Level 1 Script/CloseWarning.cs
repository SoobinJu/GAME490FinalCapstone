using UnityEngine;

public class CloseWarning : MonoBehaviour
{
    public GameObject warningPanel;

    public void ClosePanel()
    {
        warningPanel.SetActive(false);
    }
}
