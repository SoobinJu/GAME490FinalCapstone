using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L2Collectible : MonoBehaviour
{
    public string collectibleID; // Inspector에서 고유 ID 입력

    private void Start()
    {
        // 씬 로드시 이미 수집된 아이템이면 삭제
        if (PlayerPrefs.GetInt(collectibleID, 0) == 1)
        {
            Destroy(gameObject); // 이미 먹은 거면 사라짐
        }
    }

    public bool Collected()
    {
        if (PlayerPrefs.GetInt(collectibleID, 0) == 0)
        {
            PlayerPrefs.SetInt(collectibleID, 1); // 먹었다고 저장
            PlayerPrefs.Save();
            return true;
        }

        return false; // 이미 수집된 거면 다시 수집 안 함
    }
}
