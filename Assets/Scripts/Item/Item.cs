using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Item : IComparable
{
    public int Number;
    public string Name;
    public string Description = "没有描述";
    public bool IsDisposable;//是否一次性
    public int Price;
    public float CD;
    public string PicPath;

    public RareLevel rareLevel = RareLevel.E;

    public int EffectNumber;//效果编号（可能用不到）

    public int CompareTo(object obj)
    {
        Item p = obj as Item;
        return this.Number.CompareTo(p.Number);
    }
}
