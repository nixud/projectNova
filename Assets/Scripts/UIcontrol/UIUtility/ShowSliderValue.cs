using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowSliderValue : MonoBehaviour
{
    public Slider slider;
    public Text text;
    
    void Update()
    {
        text.text = ((int) (slider.value * 100)).ToString();
    }
}
