using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleControl : MonoBehaviour
{
    public string settingItem;
    public Toggle toggle;

    public void OnValueChange()
    {
        if (settingItem == "AutoFire")
            Settings.Instance.AutoFire = toggle.isOn;
        else if (settingItem == "LeftHanded")
            Settings.Instance.LeftHanded = toggle.isOn;
    }
}
