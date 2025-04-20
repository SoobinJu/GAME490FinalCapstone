using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gumiho_mini : MonoBehaviour
{
    public Sprite frontSprite;
    public Sprite backSprite;
    public float switchInterval = 3f;

    private SpriteRenderer sr;
    public bool isWatching = true;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(SwitchDirectionRoutine());
    }

    IEnumerator SwitchDirectionRoutine()
    {
        while (true)
        {
            isWatching = !isWatching;
            sr.sprite = isWatching ? frontSprite : backSprite;
            yield return new WaitForSeconds(switchInterval);
        }
    }
}
