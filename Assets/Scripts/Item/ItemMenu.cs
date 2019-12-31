#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ItemMenu : EditorWindow
{
    static List<Item> items;

    static Item item;

    static string[] selStrings;

    static bool SaveError = false;
    //static bool SaveErrorTemp = false;
    //static bool NoWeapon = false;

    static int selGridInt = 0;
    static int selGridInttemp = 0;

    //RareLevel rareLevel = RareLevel.E;

    [MenuItem("Nova/ItemEditor", false, 0)]
    static void Init()
    {
        ItemMenu ItemMenu = new ItemMenu();
        InitValue();
        ItemMenu window = (ItemMenu)GetWindow(typeof(ItemMenu));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("物品参数", EditorStyles.largeLabel);
        item.Number = EditorGUILayout.IntField("物品编号", item.Number);

        item.Name = EditorGUILayout.TextField("物品名称", item.Name);
        GUILayout.Label("物品描述");
        item.Description = EditorGUILayout.TextArea(item.Description, GUILayout.MaxHeight(75));

        if(!(item.IsDisposable = EditorGUILayout.Toggle("是否为一次性", item.IsDisposable)))
            item.CD = EditorGUILayout.FloatField("物品CD", item.CD);

        item.PicPath = EditorGUILayout.TextField("图标名", item.PicPath);

        item.Price = EditorGUILayout.IntField("物品价格", item.Price);

        item.EffectNumber = EditorGUILayout.IntField("效果编号（如果用得到的话）", item.EffectNumber);

        GUILayout.BeginVertical("Box");
        selGridInttemp = selGridInt;
        selGridInt = GUILayout.SelectionGrid(selGridInt, selStrings, 1);
        if (selGridInt != selGridInttemp)
        {
            item = items[selGridInt];
        }
        GUILayout.EndVertical();

        GUILayout.Label("稀有度：" + item.rareLevel.ToString());
        if (GUILayout.Button("稀有度向上"))
        { item.rareLevel++; }
        if (GUILayout.Button("稀有度向下"))
        { item.rareLevel--; }

        if (SaveError) EditorGUILayout.HelpBox("输入内容不正确。请检查编号是否重复，编号是否为空等", MessageType.Error);

        if (GUILayout.Button("添加新物品"))
        {
            AddItem();
        }
        if (GUILayout.Button("保存物品信息"))
        {
            SaveData(item);
        }
        if (GUILayout.Button("删除物品信息"))
        {
            DeleteData(selGridInt,items);
        }
    }

    public static void InitValue() {
        items = LoadData();
        selStrings = new string[items.Count];
        for (int i = 0; i < items.Count; i++)
            selStrings[i] = items[i].Number.ToString();
        if (items.Count >= 1)
            item = items[0];
        else
        {
            AddItem();
            item = items[0];
        }
    }

    static void AddItem()
    {
        SaveError = false;

        item = new Item();
        item.Number = 0;
        items.Add(item);
        items = ItemSort();

        selStrings = new string[items.Count];
        for (int i = 0; i < items.Count; i++)
            selStrings[i] = items[i].Number.ToString();
        selGridInt = 0;
    }

    public static List<Item> LoadData()
    {
        JsonLoader<Item> loader = new JsonLoader<Item>();
        return loader.LoadData();
    }

    public Item LoadData(int ItemNum)
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

    public void SaveData(Item item)
    {
        JsonLoader<Item> loader = new JsonLoader<Item>();
        List<Item> itemlist = new List<Item>();

        itemlist = LoadData();

        int index = 0, i;
        for (i = 0; i < itemlist.Count; i++)
        {
            if (itemlist[i].Number == item.Number)
                break;
        }
        index = i;
        if (index != itemlist.Count)
        {
            itemlist.RemoveAt(index);
        }
        itemlist.Insert(index, item);

        loader.SaveData(itemlist);

        selStrings = new string[items.Count];
        for (int ii = 0; ii < items.Count; ii++)
            selStrings[ii] = items[ii].Number.ToString();
    }
    public void DeleteData(int index, List<Item> itemlist)
    {
        SaveError = false;

        SaveData(item);

        SaveError = false;

        itemlist.RemoveAt(index);
        JsonLoader<Item> loader = new JsonLoader<Item>();

        //itemlist = ItemSort(itemlist);

        loader.SaveData(itemlist);

        items = LoadData();
        selStrings = new string[items.Count];
        for (int i = 0; i < items.Count; i++)
            selStrings[i] = items[i].Number.ToString();
        selGridInt = 0;
        item = items[selGridInt];
    }
    public static List<Item> ItemSort()
    {
        if (items.Count > 1)
            items.Sort();
        return items;
    }
}
#endif