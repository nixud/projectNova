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

    public int EffectNumber;

    public int CompareTo(object obj)
    {
        Item p = obj as Item;
        return this.Number.CompareTo(p.Number);
    }

    public ItemEffects itemEffects;

    public void LoadEffect() {
        if (EffectNumber == 1)
            itemEffects = new ShootSpeedUp();
    }

    public void Run() {
        if (itemEffects != null)
            itemEffects.Run();
    }
    public void End()
    {
        if (itemEffects != null)
            itemEffects.End();
    }
}
