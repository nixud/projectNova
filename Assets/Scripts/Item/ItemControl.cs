
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

// 设置道具栏道具
public class ItemControl : MonoBehaviour
{
    public ItemButton itemButton;

    private List<Item> _plugins = new List<Item>();
    private List<ItemStatus> _equipments = new List<ItemStatus>();
    private int _equipIndex;
    
    private Action _pluginStart, _pluginUpdate, _pluginEnd;
    void Start()
    {
        _plugins.Clear();
        _equipments.Clear();
        
        Item plugin = new Item();
        plugin.EffectNumber = 5;
        _plugins.Add(plugin);

        foreach (var p in _plugins)
        {
            _pluginStart += p.Run;
            _pluginUpdate += p.Update;
            _pluginEnd += p.End;
        }

        Item i1 = new Item();
        i1.PicPath = @"ItemButton/ItemButton4";
        i1.EffectNumber = 2;
        i1.Type = ItemType.Consume;
        
        Item i2 = new Item();
        i2.PicPath = @"ItemButton/ItemButton2";
        i2.EffectNumber = 1;
        i2.Type = ItemType.Accumulate;

        Item i3 = new Item();
        i3.PicPath = @"ItemButton/ItemButton3";
        i3.EffectNumber = 3;
        i3.Type = ItemType.Consume;

        Item i4 = new Item();
        i4.PicPath = @"ItemButton/ItemButton5";
        i4.EffectNumber = 4;
        i4.Type = ItemType.Accumulate;
        
        
        GetEquipment(i1);
        GetEquipment(i2);
        GetEquipment(i3);
        GetEquipment(i4);

        _pluginStart?.Invoke();
    }

    private void Update()
    {
        _pluginUpdate?.Invoke();
    }

    public void OnEnd()
    {
        _pluginEnd?.Invoke();
    }

    public void GetPlugins(List<Item> plugins)
    {
        foreach (var p in plugins)
        {
            if (p.Type != ItemType.Plugin)
                throw new Exception("item type not match");
            else
                _plugins.Add(p);
        }
    }

    public void GetEquipment(Item equipment)
    {
        if (equipment.Type != ItemType.Plugin)
        {
            if (equipment.Type == ItemType.Accumulate)
                _equipments.Add(new ItemStatus(equipment));
            else
                _equipments.Add(new ItemStatus(item: equipment, effectCount:((IConsume)equipment.ItemEffects).EffectCount));
            ClearEquipList(_equipIndex);
            _equipIndex = _equipments.Count - 1;
            SetItem(_equipIndex);
        }
        else
            throw new Exception("item type not match");
    }

    public void ChangeEquipment()
    {
        // if (_equipments.Count == 0)
        // {
        //     itemButton.SetItem(null);
        //     return;
        // }
        itemButton.SaveItemStatus();
        _equipIndex += ClearEquipList(_equipIndex);
        if (_equipIndex >= _equipments.Count)
            _equipIndex = 0;
        SetItem(_equipIndex);
    }

    public void GetAccumulate()
    {
        itemButton.GetAccumulation(256);
    }
    
    // test
    public void AddEquip1()
    {
        
    }

    private int ClearEquipList(int index)
    {
        if (_equipments.Count <= index || _equipments[index] == null)
            return 0;
        ItemStatus eqTmp = _equipments[index];
        if (eqTmp.item.Type == ItemType.Consume && eqTmp.effectCount == 0)
        {
            _equipments.Remove(eqTmp);
            return 0;
        }

        return 1;
    }

    private void SetItem(int index)
    {
        if (_equipments.Count <= index || _equipments[index] == null)
            itemButton.SetItem(null);
        else
            itemButton.SetItem(_equipments[index]);
    }
}

public class ItemStatus
{
    public Item item;
    
    // 消耗性道具状态
    public int effectCount;
    // 充能型道具状态
    public int accumulate;

    public ItemStatus(Item item, int effectCount = 0, int accumulate = 0)
    {
        this.item = item;
        this.effectCount = effectCount;
        this.accumulate = accumulate;
    }
}
