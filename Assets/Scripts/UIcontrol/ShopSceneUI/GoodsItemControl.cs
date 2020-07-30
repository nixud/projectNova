using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoodsItemControl : MonoBehaviour
{
    // 道具信息相关
    public String picPath;
    public String itemName;
    public String itemType;
    public int price;
    
    // other
    public Text priceText;
    public Text nameText;
    
    public Toggle toggle;
    public Image Background;
    void Start()
    {
        priceText.text = "\u20AC" + price;
        nameText.text = "[" + itemType +  "]" + itemName;
    }

    public void OnValueChange()
    {
        // 选中时sprite变换
        if (toggle.isOn)
        {
            Background.color = Color.white;
        }
        else
        {
            Background.color = new Color(1, 1, 1, (float)130/255);
        }
    }
}
