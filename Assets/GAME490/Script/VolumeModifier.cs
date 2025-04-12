using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeModifier : MonoBehaviour
{
    public enum AudioType
    {
        SFX,
        BGM
    }

    public AudioType audioType = AudioType.SFX;
    public bool playOnStart = false;

    private AudioSource audioSource;
    private float maxVolume;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        maxVolume = audioSource.volume;
        UpdateVolume();

        if (playOnStart)
        {
            audioSource.Play();
        }
    }

    public void UpdateVolume()
    {
        if (audioSource == null) return;

        if (audioType == AudioType.SFX)
        {
            audioSource.volume = maxVolume * VolumeSettings.sfxVolume;
        }
        else if (audioType == AudioType.BGM)
        {
            audioSource.volume = maxVolume * VolumeSettings.bgmVolume;
        }
    }
}
