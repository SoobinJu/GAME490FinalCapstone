using UnityEngine;

public class PlayerPositionManager : MonoBehaviour
{
    public Transform defaultSpawnPoint; // Default spawn point for new scenes
    public LayerMask groundLayer; // Ground detection

    private void Start()
    {
        // Check if the player is returning from another scene
        if (PlayerPrefs.GetInt("Returning", 0) == 1)
        {
            float x = PlayerPrefs.GetFloat("PlayerX", transform.position.x);
            float y = PlayerPrefs.GetFloat("PlayerY", transform.position.y);

            Vector3 newPosition = new Vector3(x, y, transform.position.z);

            // Check if there's ground below the saved position
            if (IsGrounded(newPosition))
            {
                transform.position = newPosition; // Move player to the saved position
            }
            else
            {
                // If the saved position is in the air, use the default spawn point
                transform.position = defaultSpawnPoint.position;
            }

            PlayerPrefs.SetInt("Returning", 0); // Reset returning flag
            PlayerPrefs.Save(); // Clear return data
        }
        else
        {
            // If not returning, start at the default spawn point
            transform.position = defaultSpawnPoint.position;
        }
    }

    private bool IsGrounded(Vector3 position)
    {
        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.down, 1f, groundLayer);
        return hit.collider != null;
    }
}
