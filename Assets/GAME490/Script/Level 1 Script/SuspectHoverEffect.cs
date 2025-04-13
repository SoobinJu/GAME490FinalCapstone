using UnityEngine;
using UnityEngine.UI; // ✅ Import UI for Image
using UnityEngine.EventSystems;

public class SuspectHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image suspectImage; // Change from SpriteRenderer to Image
    public Color hoverColor = Color.gray; // Color on hover
    private Color originalColor; // Store original color

    private void Start()
    {
        suspectImage = GetComponent<Image>(); // ✅ Get Image instead of SpriteRenderer
        if (suspectImage != null)
        {
            originalColor = suspectImage.color;
        }
    }

    /*private void OnMouseEnter()
    {
        if (suspectImage != null)
        {
            suspectImage.color = hoverColor;
        }
    }

    private void OnMouseExit()
    {
        if (suspectImage != null)
        {
            suspectImage.color = originalColor;
        }
    }*/

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (suspectImage != null)
        {
            suspectImage.color = hoverColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (suspectImage != null)
        {
            suspectImage.color = originalColor;
        }
    }
}