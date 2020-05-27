using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class SliderControl : MonoBehaviour
{
    public string settingItem;
    public Slider slider;
    
    public void OnValueChange()
    {
        if (settingItem == "MainVolume")
        {
            Settings.Instance.MainVolume = (int) (slider.value * 100);
            Debug.Log(Settings.Instance.MainVolume);
        }
        else if (settingItem == "MusicVolume")
        {
            Settings.Instance.MusicVolume = (int) (slider.value * 100);
            Debug.Log(Settings.Instance.MusicVolume);
        }
        else if (settingItem == "SoundVolume")
        {
            Settings.Instance.SoundVolume = (int) (slider.value * 100);
            Debug.Log(Settings.Instance.SoundVolume);
        }
    }
}
