using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class ChangeToggleMark : MonoBehaviour
{
    public Sprite toggleOff;
    public Sprite toggleOn;

    public Image mark;
    public Toggle toggle;
    void Start()
    {
        if (toggle.isOn)
        {
            mark.sprite = toggleOn;
        }
        else
        {
            mark.sprite = toggleOff;
        }
    }

    public void OnValueChange()
    {
        if (toggle.isOn)
        {
            mark.sprite = toggleOn;
        }
        else
        {
            mark.sprite = toggleOff;
        }
    }
}
