using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string lastTrigger = PlayerPrefs.GetString("LastEnteredTrigger", "");

        if (lastTrigger == "A")
        {
            GameObject aTrigger = GameObject.Find("EntranceA"); // A 트리거 오브젝트 이름
            if (aTrigger != null) Destroy(aTrigger);
        }
        else if (lastTrigger == "B")
        {
            GameObject bTrigger = GameObject.Find("EntranceB"); // B 트리거 오브젝트 이름
            if (bTrigger != null) Destroy(bTrigger);
        }

        PlayerPrefs.DeleteKey("LastEnteredTrigger"); // 다음 번에는 지우기
    }
}
