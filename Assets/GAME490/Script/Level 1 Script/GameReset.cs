using UnityEngine;
using TMPro;

public class GameReset : MonoBehaviour
{
    public TextMeshProUGUI chancesText;
    private int totalChances = 3; // Default number of chances

    public void ResetGame()
    {
        Debug.Log("🔄 Restarting Game! Resetting player position and chances...");

        // 🛑 Reset saved player position (ONLY when restarting)
        PlayerPrefs.DeleteKey("LastExitX");
        PlayerPrefs.DeleteKey("LastExitY");
        PlayerPrefs.DeleteKey("ReturningFromBuilding");

        // ✅ Reset chances ONLY when restarting the game
        PlayerPrefs.SetInt("Chances", 3);
        PlayerPrefs.Save();

        // ✅ ALSO reset clue/quiz progress
        GameProgressTracker.Instance.ResetProgress(); // 💥 THIS!

        // Update the UI immediately
        totalChances = 3;
        chancesText.text = "Chances: " + totalChances + "/3";
    }

}
