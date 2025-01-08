using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject level1PlayerPrefab; // Level 1 �÷��̾� ������
    public GameObject level2PlayerPrefab; // Level 2 �÷��̾� ������

    private GameObject currentPlayer;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void SpawnPlayer(Vector3 position, int level)
    {
        // ���� �÷��̾� ����
        if (currentPlayer != null)
        {
            Destroy(currentPlayer);
        }

        // ������ ���� ������ �÷��̾� ����
        if (level == 1)
        {
            currentPlayer = Instantiate(level1PlayerPrefab, position, Quaternion.identity);
        }
        else if (level == 2)
        {
            currentPlayer = Instantiate(level2PlayerPrefab, position, Quaternion.identity);
        }
    }




    //----------
    public float currentScore = 0f;
    public bool isPlaying = false;
    private void Update()
    {
        if (isPlaying)
        {
            currentScore += Time.deltaTime;
        }
        //if (Input.GetKeyDown("k"))
        //{
        //    isPlaying = true;
        //}
    }
    public void GameOver()
    {
        currentScore = 0;
    }
    public string PrettyScore()
    {
        return Mathf.RoundToInt(currentScore).ToString();
    }
}
