using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("------------Audio Source------------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("=-------------- Audio Clip ----------")]
    public AudioClip background;
    public AudioClip death;
    public AudioClip collectSFX;
    public AudioClip btn;
    public AudioClip obstacle1;
    public AudioClip obstacle2;

    // Start is called before the first frame update
    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
