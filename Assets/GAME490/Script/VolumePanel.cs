using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumePanel : MonoBehaviour
{
    public Slider sfxVolumeSlider;
    public Slider bgmVolumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        sfxVolumeSlider.value = VolumeSettings.sfxVolume;
        bgmVolumeSlider.value = VolumeSettings.bgmVolume;
    }

    public void OnSFXSliderChanged(float sliderValue)
    {
        VolumeSettings.sfxVolume = sliderValue;

        VolumeModifier[] modifiers = FindObjectsOfType(typeof(VolumeModifier)) as VolumeModifier[];

        foreach(VolumeModifier vm in modifiers)
        {
            if (vm.audioType == VolumeModifier.AudioType.SFX)
            {
                vm.UpdateVolume();
            }
        }
    }

    public void OnBGMSliderChanged(float sliderValue)
    {
        VolumeSettings.bgmVolume = sliderValue;

        VolumeModifier[] modifiers = FindObjectsOfType(typeof(VolumeModifier)) as VolumeModifier[];

        foreach (VolumeModifier vm in modifiers)
        {
            if (vm.audioType == VolumeModifier.AudioType.BGM)
            {
                vm.UpdateVolume();
            }
        }
    }

}
