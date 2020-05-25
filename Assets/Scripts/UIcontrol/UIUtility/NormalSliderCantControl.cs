using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 普通滑条
public class NormalSliderCantControl : MonoBehaviour
{
    public Image fillBlock;

    // ValueTo helper
    private bool _changeValue = false;
    private float _rate;
    private float _time;
    
    [HideInInspector]
    public float Slide
    {
        get => fillBlock.fillAmount;
        set => fillBlock.fillAmount = value > 1f ? 1f : value;
    }

    
    /// <summary>
    /// 是滑动条从现在的值平滑(线性)过渡到目标值
    /// </summary>
    /// <param name="target">目标值</param>
    /// <param name="t">时间</param>
    public void ValueTo(float target, float t)
    {
        _changeValue = true;
        _rate = (target - Slide) / t;
        _time = t;
    }
    

    private void OnEnable()
    {
        Slide = 0f;
    }

    private void Update()
    {
        if (_changeValue)
        {
            Slide += _rate * Time.deltaTime;
            if (_time <= 0f)
                _changeValue = false;
            _time -= Time.deltaTime;
        }
    }
}
