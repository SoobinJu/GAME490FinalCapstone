using UnityEngine;
using UnityEngine.UI; 

public class CluePopupHandler : MonoBehaviour
{
    public GameObject cluePanel; 
    public Image closeButtonImage; 

    private void Start()
    {

        closeButtonImage.GetComponent<Button>().onClick.AddListener(CloseCluePanel);
    }

    private void OnMouseDown()
    {

        if (cluePanel != null)
        {
            cluePanel.SetActive(true);
        }
    }

    public void CloseCluePanel()
    {

        if (cluePanel != null)
        {
            cluePanel.SetActive(false);
        }
    }
}

