using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//所有道具的基类。道具通过继承它实现。
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

    private Item LoadEffect() {
        if (EffectNumber == 1)
            itemEffects = new ShootSpeedUp();
        else if (EffectNumber == 2)
            itemEffects = new KillSelf();

        return this;
    }

    public void Run() {
        if (itemEffects != null)
            itemEffects.Run();
        else 
            LoadEffect().Run();
    }
    public void End()
    {
        if (itemEffects != null)
            itemEffects.End();
    }
}
