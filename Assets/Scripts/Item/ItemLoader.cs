using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemLoader
{
    private static List<Item> itemInfo;
    private static void LoadData()
    {
        JsonLoader<Item> loader = new JsonLoader<Item>();
        itemInfo = loader.LoadData();
    }
    
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
}
