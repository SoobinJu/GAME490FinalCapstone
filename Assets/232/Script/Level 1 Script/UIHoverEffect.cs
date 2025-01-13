using UnityEngine;

public class SpriteHoverEffect : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; // SpriteRenderer component reference
    private Color originalColor; // Original color of the sprite
    public Color hoverColor = Color.black; // Color when the mouse hovers over the sprite

    private void Start()
    {
        // Get the SpriteRenderer component and save its original color
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    private void OnMouseEnter()
    {
        // Change to hover color
        if (spriteRenderer != null)
        {
            spriteRenderer.color = hoverColor;
        }
    }

    private void OnMouseExit()
    {
        // Revert to the original color
        if (spriteRenderer != null)
        {
            spriteRenderer.color = originalColor;
        }
    }
}
