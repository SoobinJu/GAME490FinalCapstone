using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;

public class SuspectHandler : MonoBehaviour
{
    public TextMeshProUGUI chancesText; //  ?? ??? ?? UI ???
    public string correctSuspect = "A"; // ??? ???

    private int chances = 3; // ?? ? 

    private void Start()
    {
  
        UpdateChancesText();
    }

    private void Update()
    {
        // ??? ??
        if (Input.GetMouseButtonDown(0)) // ?? ??? ??? 
        {
            DetectSuspectClick();
        }
    }

    private void DetectSuspectClick()
    {

        // If the mouse click is over a UI element, do nothing
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return; // Ignore clicks on UI elements like the pop-up panel or buttons
        }

        // ????? ??? ??? ?? ??? ?? ??
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null)
        {
            // ??? ????? ?? 
            string clickedSuspectName = hit.collider.gameObject.name; // ??? ???? ????
            ChooseSuspect(clickedSuspectName); // ??? ??? ??? ChooseSuspect ???? ??
        }
    }

    public void ChooseSuspect(string suspectName)
    {
        if (suspectName == correctSuspect)
        {
            // ?? ??? ?????
            Debug.Log("Congratulation! going to next level...");
            SceneManager.LoadScene("Narration2"); // ?? ? ????? 
        }
        else
        {
            // ?? ???
            chances--; // -1 ??
            UpdateChancesText(); // UI ???
            Debug.Log("?????! ?? ??: " + chances);

            if (chances <= 0)
            {
                // ??? 0???
                Debug.Log("Game Over! you got no chances left.");
                SceneManager.LoadScene("LoseScene"); // ???? ?? ? 
            }
        }
    }

    private void UpdateChancesText()
    {
        // ?? ??? ?? (canvas? ?? ???)
        chancesText.text = "Chances: " + chances + "/3"; // ?? ?? ??
    }
}
