using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemLoader
{
    /// <summary>
    /// 保存加载的全部道具信息
    /// </summary>
    private static List<Item> itemInfo;

    
    /// <summary>
    /// 根据编号获取道具
    /// </summary>
    /// <param name="ItemNum"></param>
    /// <returns></returns>
    public static Item LoadData(int ItemNum)
    {
        if (itemInfo == null)
            LoadData();
        
        Item returnItem = new Item();
        for (int i = 0; i < itemInfo.Count; i++)
        {
            if (itemInfo[i].Number == ItemNum)
                returnItem = itemInfo[i];
        }

        return returnItem;
    }

    /// <summary>
    /// 保存道具更改信息（编辑器使用）
    /// </summary>
    public static void SaveData()
    {
        JsonLoader<Item> loader = new JsonLoader<Item>();
        loader.SaveData(itemInfo);
    }
    
    /// <summary>
    /// 获取道具列表（编辑器使用）
    /// </summary>
    /// <returns></returns>
    public static List<Item> GetItemList()
    {
        if (itemInfo == null)
            LoadData();

        return itemInfo;
    }
    
    /// <summary>
    /// 加载全部道具信息
    /// </summary>
    private static void LoadData()
    {
        JsonLoader<Item> loader = new JsonLoader<Item>();
        itemInfo = loader.LoadData();
        itemInfo.Sort();
    }
}
