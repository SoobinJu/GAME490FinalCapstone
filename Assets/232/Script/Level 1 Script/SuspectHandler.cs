using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SuspectHandler : MonoBehaviour
{
    public TextMeshProUGUI chancesText; // UI text for chances
    public string correctSuspect = "A"; //The correct suspect

    private int chances = 3; //total chances

    private void Start()
    {
  
        UpdateChancesText();
    }

    private void Update()
    {
        //detect mouse click
        if (Input.GetMouseButtonDown(0))
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

        //Cast a ray from the camera to the world point of the mouse position
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null)
        {
            //Check if the clicked object is a suspect
            string clickedSuspectName = hit.collider.gameObject.name; //Get the suspect's GameObject name
            ChooseSuspect(clickedSuspectName); //Pass the name to the ChooseSuspect method
        }
    }

    public void ChooseSuspect(string suspectName)
    {
        if (suspectName == correctSuspect)
        {
            //if correct suspect
            Debug.Log("Congratulation! going to next level...");
            SceneManager.LoadScene("Narration2"); // going to next scene when getting correct suspect (next scene name)
        }
        else
        {
            DecreaseChances();
        }
    }

    public void DecreaseChances()
    {
        //when player chose wrong suspect -1 chance
        chances--;
        UpdateChancesText(); //Update the UI
        Debug.Log("?????! ?? ??: " + chances);

        if (chances <= 0)
        {
            Debug.Log("Game Over! you got no chances left.");
            SceneManager.LoadScene("LoseScene"); // going to Lose scene (name of next scene)
        }
    }

    private void UpdateChancesText()
    {
        // ?? ??? ?? (canvas? ?? ???) / text that appears on canvas
        chancesText.text = "Chances: " + chances + "/3"; // ?? ?? ?? 
    }
}
