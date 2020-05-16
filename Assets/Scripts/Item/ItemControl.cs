
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.Serialization;
using UnityEngine.UI;

// 设置道具栏道具
public class ItemControl : MonoBehaviour
{
    public ItemButton itemButton;

    public List<int> EquipList;
    public List<int> PluginList;

    [HideInInspector]public int equipmentMax = 2;
    [HideInInspector]public List<Item> plugins;
    private List<ItemStatus> _equipments = new List<ItemStatus>();
    private int _equipIndex;
    
    private Action _pluginStart, _pluginUpdate, _pluginEnd;
    void Start()
    {
        SetEquipmentLimit(2);
        _equipments.Clear();

        plugins = PlayerStatus.GetInstance().Plugins;

        #region test
        plugins.Sort();
        foreach (var plugin in PluginList)
        {
            var p = ItemLoader.LoadData(plugin);
            plugins.Add(p);
        }

        foreach (var equip in EquipList)
        {
            var p = ItemLoader.LoadData(equip);
            PlayerStatus.GetInstance().Equipments.Add(p);
        }

        #endregion
        
        foreach (var p in plugins)
        {
            _pluginStart += p.Run;
            _pluginUpdate += p.Update;
            _pluginEnd += p.End;
        }
        
        foreach (var equipment in PlayerStatus.GetInstance().Equipments)
        {
            GetEquipment(equipment, false);
        }
        
        _pluginStart?.Invoke();
    }

    private void FixedUpdate()
    {
        _pluginUpdate?.Invoke();
    }

    // 关卡结束时调用
    public void OnEnd()
    {
        _pluginEnd?.Invoke();
        itemButton.OnEnd();
    }

    public void GetPlugins(List<Item> plugins)
    {
        foreach (var p in plugins)
        {
            if (p.Type != ItemType.Plugin)
                throw new Exception("item type not match");
            else
                this.plugins.Add(p);
        }
    }

    // 设置道具栏上限
    public void SetEquipmentLimit(int max)
    {
        equipmentMax = max;
    }

    // 添加道具
    public void GetEquipment(Item equipment, bool addToStatus = true)
    {
        if (equipment.Type != ItemType.Plugin)
        {
            if (_equipments.Count == equipmentMax)
            {
                _equipments[_equipIndex] = new ItemStatus(equipment);
            }
            else
            {
                _equipments.Add(new ItemStatus(equipment));
                ClearEquipList(_equipIndex);
                _equipIndex = _equipments.Count - 1;   
            }
            SetItem(_equipIndex);
            
            if (addToStatus)
                PlayerStatus.GetInstance().Equipments.Add(equipment);
        }
        else
            throw new Exception("item type not match");
    }
    
    public void ChangeEquipment()
    {
        itemButton.SaveItemStatus();
        _equipIndex += ClearEquipList(_equipIndex);
        if (_equipIndex >= _equipments.Count)
            _equipIndex = 0;
        SetItem(_equipIndex);
    }

    // 武器充能
    public void GetAccumulate(int acc)
    {
        itemButton.GetAccumulation(acc);
    }

    // 删除插件
    public void DeletePlugin(Item plugin)
    {
        if (plugin == null)
            return;

        _pluginStart -= plugin.Run;
        _pluginUpdate -= plugin.Update;
        _pluginEnd -= plugin.End;
        
        plugin.End();
        plugins.Remove(plugin);
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
    
    
    // test
    public void TestAcc()
    {
        GetAccumulate(1000);
    }
}

public class ItemStatus
{
    public Item item;
    
    // 消耗性道具状态
    public int effectCount;
    // 充能型道具状态
    public int accumulate;

    public ItemStatus(Item item)
    {
        this.item = item;
        accumulate = 0;
        effectCount = 0;
        if (item.Type == ItemType.Consume)
            effectCount = item.EffectCount;
    }
}
