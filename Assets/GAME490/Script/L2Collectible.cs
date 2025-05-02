using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L2Collectible : MonoBehaviour
{
    public string collectibleID; // Inspector¿¡¼­ °íÀ¯ ID ÀÔ·Â

    private void Start()
    {
        // ¾À ·Îµå½Ã ÀÌ¹Ì ¼öÁýµÈ ¾ÆÀÌÅÛÀÌ¸é »èÁ¦
        if (PlayerPrefs.GetInt(collectibleID, 0) == 1)
        {
            Destroy(gameObject); // ÀÌ¹Ì ¸ÔÀº °Å¸é »ç¶óÁü
        }
    }

    public bool Collected()
    {
        if (PlayerPrefs.GetInt(collectibleID, 0) == 0)
        {
            PlayerPrefs.SetInt(collectibleID, 1); // ¸Ô¾ú´Ù°í ÀúÀå
            PlayerPrefs.Save();
            return true;
        }

        return false; // ÀÌ¹Ì ¼öÁýµÈ °Å¸é ´Ù½Ã ¼öÁý ¾È ÇÔ
    }
}
