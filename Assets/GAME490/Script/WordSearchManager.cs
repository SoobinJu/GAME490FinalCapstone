using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WordSearchManager : MonoBehaviour
{
    public GameObject cellPrefab;
    public Transform gridParent;
    public int gridSize = 10;

    public List<TextMeshProUGUI> wordListTexts = new List<TextMeshProUGUI>();

    private LetterCell[,] grid;
    private List<string> wordList = new List<string>() { "Liver", "Tail", "Marble", "Norigae" };

    // 정답 영어 → 한글 딕셔너리
    public Dictionary<string, string> wordPairs = new Dictionary<string, string>()
    {
        { "tail", "꼬리" },
        { "liver", "간" },
        { "marble", "구슬" },
        { "norigae", "노리개" }
    };

    // 선택된 셀들 저장
    [HideInInspector] public List<LetterCell> selectedCells = new List<LetterCell>();

    void Start()
    {
        grid = new LetterCell[gridSize, gridSize];
        GenerateGrid();
        PlaceWords();

        Debug.Log("생성된 셀 개수: " + gridParent.childCount);
    }

    void GenerateGrid()
    {
        for (int y = 0; y < gridSize; y++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                GameObject newCell = Instantiate(cellPrefab, gridParent);
                LetterCell letterCell = newCell.GetComponent<LetterCell>();
                char randomChar = (char)Random.Range(65, 91); // A-Z
                letterCell.SetLetter(randomChar.ToString().ToUpper());
                grid[x, y] = letterCell;
            }
        }
    }

    void PlaceWords()
    {
        System.Random rng = new System.Random();

        foreach (string word in wordList)
        {
            bool placed = false;

            while (!placed)
            {
                int maxX = gridSize - word.Length;
                int x = rng.Next(0, maxX + 1);
                int y = rng.Next(0, gridSize);

                bool canPlace = true;

                // 기존 글자와 충돌하는지 검사
                for (int i = 0; i < word.Length; i++)
                {
                    string existingLetter = grid[x + i, y].letterText.text;
                    if (!string.IsNullOrEmpty(existingLetter) &&
                        existingLetter != word[i].ToString().ToUpper())
                    {
                        canPlace = false;
                        break;
                    }
                }

                if (canPlace)
                {
                    for (int i = 0; i < word.Length; i++)
                    {
                        grid[x + i, y].SetLetter(word[i].ToString().ToUpper());
                        grid[x + i, y].word = word.ToLower();
                    }
                    placed = true;
                }
            }
        }
    }

    // 정답 체크 함수
    public void CheckSelectedWord()
    {
        string selectedWord = "";
        foreach (var cell in selectedCells)
        {
            selectedWord += cell.letterText.text.ToLower();
        }

        if (wordPairs.ContainsKey(selectedWord))
        {
            string korean = wordPairs[selectedWord];
            Debug.Log($"정답! {selectedWord} → {korean}");

            foreach (var cell in selectedCells)
            {
                cell.Highlight();
            }

            StrikeThroughKoreanWord(korean);
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

    // 한국어 텍스트 취소선 처리
    public void StrikeThroughKoreanWord(string korean)
    {
        foreach (TextMeshProUGUI wordText in wordListTexts)
        {
            if (wordText.text == korean)
            {
                wordText.text = "<s>" + korean + "</s>";
                wordText.richText = true;
            }
        }
    }
}
