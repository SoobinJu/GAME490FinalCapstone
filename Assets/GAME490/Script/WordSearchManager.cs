using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WordSearchManager : MonoBehaviour
{
    public static WordSearchManager Instance;
    public QuizChance quizChance;


    public GameObject cellPrefab;
    public Transform gridParent;
    public int gridSize = 10;

    public List<TextMeshProUGUI> wordListTexts = new List<TextMeshProUGUI>();

    public TextMeshProUGUI warningText;
    public Image warningBackground;

    public TextMeshProUGUI resultText;
    public Image resultBackground;

    private LetterCell[,] grid;
    private List<string> wordList = new List<string>() { "Liver", "Tail", "Marble", "Norigae" };

    public Dictionary<string, string> wordPairs = new Dictionary<string, string>()
    {
        { "tail", "꼬리" },
        { "liver", "간" },
        { "marble", "구슬" },
        { "norigae", "노리개" }
    };

    [HideInInspector] public List<LetterCell> selectedCells = new List<LetterCell>();
    [HideInInspector] public bool selectionInProgress = false;

    private bool isAnswerRevealed = false;
    private HashSet<string> solvedWords = new HashSet<string>();

    void Start()
    {
        Instance = this;

        grid = new LetterCell[gridSize, gridSize];
        GenerateGrid();
        PlaceWords();

        // 시작할 때 메시지 비활성화
        warningText.gameObject.SetActive(false);
        warningBackground.gameObject.SetActive(false);
        resultText.gameObject.SetActive(false);
        resultBackground.gameObject.SetActive(false);
    }

    void GenerateGrid()
    {
        for (int y = 0; y < gridSize; y++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                GameObject newCell = Instantiate(cellPrefab, gridParent);
                LetterCell letterCell = newCell.GetComponent<LetterCell>();
                char randomChar = (char)Random.Range(65, 91);
                letterCell.SetLetter(randomChar.ToString().ToUpper());
                grid[x, y] = letterCell;
            }
        }
    }

    void PlaceWords()
    {
        Vector2Int[] directions = new Vector2Int[]
        {
            new Vector2Int(1, 0),
            new Vector2Int(0, 1),
        };

        System.Random rng = new System.Random();

        foreach (string word in wordList)
        {
            bool placed = false;
            int attempts = 0;
            while (!placed && attempts < 100)
            {
                attempts++;

                Vector2Int dir = directions[rng.Next(directions.Length)];
                int maxX = dir.x == 0 ? gridSize : gridSize - word.Length;
                int maxY = dir.y == 0 ? gridSize : gridSize - word.Length;

                int startX = rng.Next(0, maxX);
                int startY = rng.Next(0, maxY);

                bool canPlace = true;
                for (int i = 0; i < word.Length; i++)
                {
                    int x = startX + i * dir.x;
                    int y = startY + i * dir.y;

                    if (!string.IsNullOrEmpty(grid[x, y].word) && grid[x, y].word != word.ToLower())
                    {
                        canPlace = false;
                        break;
                    }
                }

                if (canPlace)
                {
                    for (int i = 0; i < word.Length; i++)
                    {
                        int x = startX + i * dir.x;
                        int y = startY + i * dir.y;

                        grid[x, y].SetLetter(word[i].ToString().ToUpper());
                        grid[x, y].word = word.ToLower();
                    }

                    placed = true;
                }
            }

            if (!placed)
            {
                Debug.LogWarning($"'{word}'을(를) 퍼즐에 배치할 수 없습니다.");
            }
        }
    }

    public void BeginSelection()
    {
        selectionInProgress = true;
        selectedCells.Clear();
    }

    public void EndSelection()
    {
        selectionInProgress = false;
        CheckSelectedWord();
    }

    public void CheckSelectedWord()
    {
        if (isAnswerRevealed)
        {
            foreach (var cell in selectedCells)
            {
                cell.highlightImage.SetActive(false);
            }
            selectedCells.Clear();

            ShowWarningMessage("The answers have been revealed. You can no longer play.");
            return;
        }

        string selectedWord = "";
        foreach (var cell in selectedCells)
        {
            selectedWord += cell.letterText.text.ToLower();
        }

        if (wordPairs.ContainsKey(selectedWord) && !solvedWords.Contains(selectedWord))
        {
            string korean = wordPairs[selectedWord];
            Debug.Log($"정답! {selectedWord} → {korean}");

            foreach (var cell in selectedCells)
            {
                cell.Highlight();
            }

            solvedWords.Add(selectedWord);
            StrikeThroughKoreanWord(korean);

            if (solvedWords.Count == wordList.Count)
            {
                // 기회 증가
                if (quizChance != null)
                {
                    quizChance.IncreaseChances();
                }

                ShowResultMessage("You got everything right! +1 Choice earned!");
            }
        }
        else
        {
            foreach (var cell in selectedCells)
            {
                cell.highlightImage.SetActive(false);
            }
        }

        selectedCells.Clear();
    }

    public void StrikeThroughKoreanWord(string korean)
    {
        foreach (TextMeshProUGUI wordText in wordListTexts)
        {
            if (wordText.text == korean)
            {
                wordText.text = "<s><color=red>" + korean + "</color></s>";
                wordText.richText = true;
            }
        }
    }

    public void ShowAllAnswers()
    {
        isAnswerRevealed = true;

        foreach (string word in wordList)
        {
            ShowWordOnGrid(word.ToLower());
        }
    }

    private void ShowWordOnGrid(string word)
    {
        for (int y = 0; y < gridSize; y++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                if (grid[x, y].word == word)
                {
                    grid[x, y].Highlight();
                }
            }
        }
    }

    public void ShowWarningMessage(string message)
    {
        warningText.text = message;
        warningText.gameObject.SetActive(true);
        warningBackground.gameObject.SetActive(true);
        StartCoroutine(HideWarningMessageAfterDelay(2f));
    }

    IEnumerator HideWarningMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        warningText.gameObject.SetActive(false);
        warningBackground.gameObject.SetActive(false);
    }

    public void ShowResultMessage(string message)
    {
        resultText.text = message;
        resultText.gameObject.SetActive(true);
        resultBackground.gameObject.SetActive(true);
        StartCoroutine(HideResultMessageAfterDelay(2f));
    }

    IEnumerator HideResultMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        resultText.gameObject.SetActive(false);
        resultBackground.gameObject.SetActive(false);
    }
}
