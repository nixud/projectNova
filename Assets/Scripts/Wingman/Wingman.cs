using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Wingman : IComparable
{
    public string Number;       // 僚机的主键，编号为数字,
    public string Name;         // 僚机的名字
    public string Description;  // 僚机的描述
    public string Prefab;   // 僚机所用预设
    public int Price;           // 僚机价格
    public RareLevel rareLevel; // 僚机稀有度

    public int CompareTo(object other)  //僚机比较时按编号大小比较
    {
        Wingman o = other as Wingman;
        return Convert.ToInt32(Number).CompareTo(Convert.ToInt32(o.Number));
    }
}
