#if UNITY_EDITOR

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEditor;

public class ItemMenu : EditorWindow
{
    static List<Item> items;
    private static Item item;

    private static string[] itemNameList;
    private static int[] itemIndexList;
    private static int itemIndex = 0;
    private static int itemIndexTemp;
    
    [MenuItem("Nova/ItemEditor", false, 0)]
    static void Init()
    {
        ItemMenu itemMenu = GetWindow<ItemMenu>();
        LoadData();
        RefreshWindow();
        
        
        itemMenu.Show();
    }

    private void OnGUI()
    {
        itemIndex = EditorGUILayout.IntPopup(itemIndex, itemNameList, itemIndexList);
        if (itemIndexTemp != itemIndex)
        {
            itemIndexTemp = itemIndex;
            item = items[itemIndex];
        }
        
        DrawItemBox();
    }

    private static void DrawItemBox()
    {
        GUILayout.BeginVertical("Box");
        GUILayout.Label("道具属性");
        item.Number = EditorGUILayout.IntField("Number", item.Number);
        item.Name = EditorGUILayout.TextField("Name", item.Name);
        GUILayout.Label("Description");
        item.Description = EditorGUILayout.TextArea(item.Description);
        item.Type = (ItemType)EditorGUILayout.EnumPopup("类型", item.Type);

        item.rareLevel = (RareLevel) EditorGUILayout.EnumPopup("稀有度", item.rareLevel);
        
        if (item.Type == ItemType.Accumulate)
        {
            item.Accumulate = EditorGUILayout.IntField("充能值", item.Accumulate);
        }
        else if (item.Type == ItemType.Consume)
        {
            item.EffectCount = EditorGUILayout.IntField("使用次数", item.EffectCount);
            item.Cd = EditorGUILayout.FloatField("CD", item.Cd);
        }

        item.EffectName = GetTypeStr(item.Type) + item.Number.ToString();
        EditorGUILayout.TextField("效果脚本名", item.EffectName);
        item.PicPath = EditorGUILayout.TextField("PicPath", item.PicPath);
        GUILayout.EndVertical();

        if (GUILayout.Button("添加道具"))
        {
            AddItem();
        }

        if (GUILayout.Button("删除道具"))
        {
            DeleteItem();
        }

        if (GUILayout.Button("保存"))
        {
            SaveData();
            RefreshWindow();
        }

        if (GUILayout.Button("刷新"))
        {
            RefreshWindow();
            items.Sort();
        }
    }

    private static void DeleteItem()
    {
        items.Remove(item);
        itemIndex = 0;
        item = items[itemIndex];
    }

    private static void AddItem()
    {
        items.Add(new Item());
        itemIndex = items.Count - 1;
        item = items[itemIndex];
    }

    private static void RefreshWindow()
    {
        itemNameList = new string[items.Count];
        itemIndexList = new int[items.Count];
        for (int i = 0; i < items.Count; i++)
        {
            itemNameList[i] = "[" + items[i].Number.ToString() + "]" + items[i].Name;
            itemIndexList[i] = i;
        }

        itemIndex = 0;
        item = items[itemIndex];
    }

    private static void SaveData()
    {
        ItemLoader.SaveData();
    }

    private static void LoadData()
    {
        items = ItemLoader.GetItemList();
    }

    private static string GetTypeStr(ItemType type)
    {
        if (type == ItemType.Plugin)
            return "plugin_";
        else
            return "equip_";
    }
}
#endif