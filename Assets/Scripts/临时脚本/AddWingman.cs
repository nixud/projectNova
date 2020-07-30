using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddWingman : MonoBehaviour
{
    public Text desc;
    public string d;
    
    public List<string> wingmansNumbers;
    public Color pressColor, normalColor;

    private void Start()
    {
        GetComponent<Image>().color = normalColor;
    }

    public void OnClick()
    {
        if (GetComponent<Toggle>().isOn)
        {
            desc.text = d;
            PlayerSelect.Instance.wingmansNumbers = wingmansNumbers;
            GetComponent<Image>().color = pressColor;
        }
        else
        {
            PlayerSelect.Instance.wingmansNumbers = new List<string>();
            GetComponent<Image>().color = normalColor;
        }
    }
}
