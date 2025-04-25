using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L2Collectible : MonoBehaviour
{
    public string collectibleID; // Inspector���� ���� ID �Է�

    private void Start()
    {
        // �� �ε�� �̹� ������ �������̸� ����
        if (PlayerPrefs.GetInt(collectibleID, 0) == 1)
        {
            Destroy(gameObject); // �̹� ���� �Ÿ� �����
        }
    }

    public bool Collected()
    {
        if (PlayerPrefs.GetInt(collectibleID, 0) == 0)
        {
            PlayerPrefs.SetInt(collectibleID, 1); // �Ծ��ٰ� ����
            PlayerPrefs.Save();
            return true;
        }

        return false; // �̹� ������ �Ÿ� �ٽ� ���� �� ��
    }
}
