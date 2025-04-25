using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gumiho_mini : MonoBehaviour
{
    public Sprite frontSprite;
    public Sprite backSprite;
    public float switchInterval = 1.5f;

    private SpriteRenderer sr;
    
    public bool isWatching = true;
    public bool stopWatching = false;

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
            if (!stopWatching)
            {
                isWatching = !isWatching;
                sr.sprite = isWatching ? frontSprite : backSprite;
            }
           
            yield return new WaitForSeconds(switchInterval);
        }
    }

    public void StopWatching()
    {
        stopWatching = true;
        isWatching = false;
        sr.sprite = backSprite;
    }
}
