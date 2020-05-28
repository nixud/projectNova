using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipCanvasControl : MonoBehaviour
{
    private readonly static string Prefab = @"Prefabs/UIPrefabs/EquipCanvas";
    private GameObject _equipCanvas;

    public GameObject EquipCanvas
    {
        get
        {
            if (_equipCanvas == null)
            {
                _equipCanvas = Resources.Load<GameObject>(Prefab);
                _equipCanvas = Instantiate(_equipCanvas);
                _equipCanvas.name = "EquipCanvas";
                _equipCanvas.SetActive(false);
            }

            return _equipCanvas;
        }
    }

    public void OnClick()
    {
        EquipCanvas.SetActive(!EquipCanvas.activeSelf);
        if (EquipCanvas.activeSelf)
        {
            Time.timeScale = 0;
        }
        else
            Time.timeScale = 1f;
    }
}
