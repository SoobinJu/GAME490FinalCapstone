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
            GameObject aTrigger = GameObject.Find("EntranceA"); // A Ʈ���� ������Ʈ �̸�
            if (aTrigger != null) Destroy(aTrigger);
        }
        else if (lastTrigger == "B")
        {
            GameObject bTrigger = GameObject.Find("EntranceB"); // B Ʈ���� ������Ʈ �̸�
            if (bTrigger != null) Destroy(bTrigger);
        }

        PlayerPrefs.DeleteKey("LastEnteredTrigger"); // ���� ������ �����
    }
}
