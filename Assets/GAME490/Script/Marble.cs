using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marble : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip brokenSound;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void BrokenSound()
    {
        audioSource.PlayOneShot(brokenSound);
    }
}
