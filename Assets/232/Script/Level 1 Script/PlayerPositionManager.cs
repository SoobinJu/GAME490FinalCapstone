using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPositionManager : MonoBehaviour
{
    public Transform defaultSpawnPoint; // Default spawn point (if no saved position)
    public LayerMask groundLayer; // Ground detection

    private void Start()
    {
        // Check if the player is returning from a building
        if (PlayerPrefs.GetInt("Returning", 0) == 1)
        {
            float x = PlayerPrefs.GetFloat("LastEntranceX", transform.position.x);
            float y = PlayerPrefs.GetFloat("LastEntranceY", transform.position.y);
            string lastBuilding = PlayerPrefs.GetString("LastBuilding", "");

            if (lastBuilding == SceneManager.GetActiveScene().name) // Check if we returned to the correct scene
            {
                Vector3 newPosition = new Vector3(x, y, transform.position.z);

                // Check if there's ground below the saved position
                if (IsGrounded(newPosition))
                {
                    transform.position = newPosition; // Move player to the saved position
                }
                else
                {
                    transform.position = defaultSpawnPoint.position; // Use default spawn point if no ground
                }
            }

            PlayerPrefs.SetInt("Returning", 0); // Reset returning flag
            PlayerPrefs.Save(); // Clear return data
        }
        else
        {
            transform.position = defaultSpawnPoint.position; // Start at default position
        }
    }

    private bool IsGrounded(Vector3 position)
    {
        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.down, 1f, groundLayer);
        return hit.collider != null;
    }
}
