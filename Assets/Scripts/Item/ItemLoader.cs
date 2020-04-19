using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemLoader 
{
    public static List<Item> LoadData()
    {
        JsonLoader<Item> loader = new JsonLoader<Item>();
        return loader.LoadData();
    }
    
    public static Item LoadData(int ItemNum)
    {
        JsonLoader<Item> loader = new JsonLoader<Item>();
        List<Item> itemlist = new List<Item>();

        itemlist = loader.LoadData();

        Item returnItem = new Item();
        for (int i = 0; i < itemlist.Count; i++)
        {
            if (itemlist[i].Number == ItemNum)
                returnItem = itemlist[i];
        }

        return returnItem;
    }
}
