using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LetterCell : MonoBehaviour
{
    public TextMeshProUGUI letterText;
    public GameObject highlightImage;
    public string word = "";

    public void SetLetter(string letter)
    {
        letterText.text = letter;
    }

    public void Highlight()
    {
        highlightImage.SetActive(true);
    }
}
