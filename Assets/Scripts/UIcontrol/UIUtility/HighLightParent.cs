using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighLightParent : MonoBehaviour
{
    private Color temp;

    public void OnPointerDown()
    {
        temp = GetComponent<Image>().color;
        var nowColor = temp;
        nowColor.a = 1;
        GetComponent<Image>().color = nowColor;
    }

    public void OnPointerUp()
    {
        GetComponent<Image>().color = temp;
    }
}
