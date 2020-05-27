using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanelControl : MonoBehaviour
{
    public Slider mainVolumeSlider, musicVolumeSlider, soundVolumeSlider;
    public Toggle autoFireToggle, leftHandedToggle;
    
    private void OnEnable()
    {
        mainVolumeSlider.value = Settings.Instance.MainVolume / (float)100;
        musicVolumeSlider.value = Settings.Instance.SoundVolume / (float)100;
        soundVolumeSlider.value = Settings.Instance.MusicVolume / (float)100;
        autoFireToggle.isOn = Settings.Instance.AutoFire;
        leftHandedToggle.isOn = Settings.Instance.LeftHanded;
    }
}