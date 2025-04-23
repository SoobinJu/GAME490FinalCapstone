using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState Instance;

    // 플레이어 정보
    public Vector3 savedPlayerPosition;
    public float savedPlayerHealth;

    // 단서 & 퀴즈 진행도
    public int savedCluesFound;
    public int savedQuizzesSolved;

    // 찬스 남은 개수
    public int savedChances;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 바뀌어도 살아있음
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
