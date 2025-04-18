using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class LetterCell : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerUpHandler
{
    public TextMeshProUGUI letterText;
    public GameObject highlightImage;
    public string word;

    public void SetLetter(string letter)
    {
        letterText.text = letter;
    }

    public void Highlight()
    {
        highlightImage.SetActive(true);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        WordSearchManager.Instance.BeginSelection();
        SelectThisCell();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (WordSearchManager.Instance.selectionInProgress)
        {
            SelectThisCell();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        WordSearchManager.Instance.EndSelection();
    }

    void SelectThisCell()
    {
        if (!WordSearchManager.Instance.selectedCells.Contains(this))
        {
            WordSearchManager.Instance.selectedCells.Add(this);
            Highlight();
        }
    }
}
