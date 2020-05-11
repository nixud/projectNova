using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aaaaaa : MonoBehaviour
{
    private void Start()
    {
        Item item = new Item();
        item.Type = ItemType.Accumulate;
        item.Name = "冷却系统过载";
        item.Description = "";
        item.Accumulate = 800;
        item.EffectName = "ShootSpeedUp";
        item.Number = 101;
        item.Price = 50;
        item.rareLevel = RareLevel.A;

        var list = ItemLoader.GetItemList();
        list.Add(item);
        // ItemLoader.SaveData();
    }
}
