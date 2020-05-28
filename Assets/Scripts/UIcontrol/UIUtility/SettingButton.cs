using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingButton : MonoBehaviour
{
    private readonly static string Prefab = @"Prefabs/UIPrefabs/SettingCanvas";
    private GameObject _settingCanvas;

    public GameObject SettingCanvas
    {
        get
        {
            if (_settingCanvas == null)
            {
                _settingCanvas = Resources.Load<GameObject>(Prefab);
                _settingCanvas = Instantiate(_settingCanvas);
                // _settingCanvas.GetComponent<Canvas>().worldCamera = Camera.main;
                _settingCanvas.SetActive(false);
            }

            return _settingCanvas;
        }
    }

    public void OnClick()
    {
        SettingCanvas.SetActive(!SettingCanvas.activeSelf);
    }
}
