using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using Newtonsoft.Json;

//所有道具的基类。道具通过继承它实现。
public class Item : IComparable
{
    // 
    public ItemType Type;
    
    public int Number;
    public string Name;
    public string Description = "没有描述";
    public int Price;
    public string PicPath;

    public RareLevel rareLevel = RareLevel.E;

    public string EffectName;

    public int Accumulate;
    public int EffectCount;
    public float Cd;

    public int CompareTo(object obj)
    {
        Item p = obj as Item;
        return this.Number.CompareTo(p.Number);
    }

    private ItemEffects _itemEffects;

    [JsonIgnore] public ItemEffects ItemEffects
    {
        get
        {
            if (_itemEffects == null)
                LoadEffect();
            return _itemEffects;
        }
    }

    private void LoadEffect() {
        // if (EffectNumber == 1)
        //     _itemEffects = new ShootSpeedUp();
        // else if (EffectNumber == 2)
        //     _itemEffects = new KillSelf();
        // else if (EffectNumber == 3)
        //     _itemEffects = new Equip1();
        // else if (EffectNumber == 4)
        //     _itemEffects = new Equip2();
        // else if (EffectNumber == 5)
        //     _itemEffects = new plugin1();
        
        // 使用反射
        Assembly assembly = Assembly.GetExecutingAssembly();
        _itemEffects = assembly.CreateInstance(EffectName) as ItemEffects;
    }

    public void Run() {
        ItemEffects.Run();
    }

    public void Update()
    {
        ItemEffects.Update();
    }
    
    public void End()
    {
        ItemEffects.End();
    }
}