using UnityEngine;

public class SuspectHoverEffect : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; 
    private Color originalColor; 
    public Color hoverColor = Color.gray; // color change 
    private void Start()
    {
 
        spriteRenderer = GetComponent<SpriteRenderer>();
      
        originalColor = spriteRenderer.color;
    }

    private void OnMouseEnter()
    {
       
        spriteRenderer.color = hoverColor;
    }

    private void OnMouseExit()
    {
      
        spriteRenderer.color = originalColor;
    }
}
