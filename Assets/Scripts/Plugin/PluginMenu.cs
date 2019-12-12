#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PluginMenu : EditorWindow
{
    static List<Plugin> plugins;

    static Plugin plugin;

    static string[] selStrings;

    static bool SaveError = false;
    static bool SaveErrorTemp = false;
    static bool NoWeapon = false;

    static int selGridInt = 0;
    static int selGridInttemp = 0;

    [MenuItem("Nova/PluginEditor", false, 0)]
    static void Init()
    {
        PluginMenu PluginMenu = new PluginMenu();
        InitValue();
        PluginMenu window = (PluginMenu)GetWindow(typeof(PluginMenu));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("插件参数", EditorStyles.largeLabel);
        plugin.Number = EditorGUILayout.TextField("插件编号", plugin.Number);

        plugin.Name = EditorGUILayout.TextField("插件名称", plugin.Name);
        GUILayout.Label("插件描述");
        plugin.Description = EditorGUILayout.TextArea(plugin.Description, GUILayout.MaxHeight(75));

        plugin.PicPath = EditorGUILayout.TextField("图标名", plugin.PicPath);

        plugin.Price = EditorGUILayout.IntField("插件价格", plugin.Price);

        GUILayout.BeginVertical("Box");
        selGridInttemp = selGridInt;
        selGridInt = GUILayout.SelectionGrid(selGridInt, selStrings, 1);
        if (selGridInt != selGridInttemp)
        {
            plugin = plugins[selGridInt];
        }
        GUILayout.EndVertical();

        GUILayout.Label("稀有度：" + plugin.rareLevel.ToString());
        if (GUILayout.Button("稀有度向上"))
        { plugin.rareLevel++; }
        if (GUILayout.Button("稀有度向下"))
        { plugin.rareLevel--; }

        if (SaveError) EditorGUILayout.HelpBox("输入内容不正确。请检查编号是否重复，编号是否为空等", MessageType.Error);

        if (GUILayout.Button("添加新插件"))
        {
            AddPlugin();
        }
        if (GUILayout.Button("保存插件信息"))
        {
            SaveData(plugin);
        }
        if (GUILayout.Button("删除插件信息"))
        {
            DeleteData(selGridInt, plugins);
        }
    }

    public static void InitValue()
    {
        plugins = LoadData();
        selStrings = new string[plugins.Count];
        for (int i = 0; i < plugins.Count; i++)
            selStrings[i] = plugins[i].Number.ToString();
        if (plugins.Count >= 1)
            plugin = plugins[0];
        else
        {
            AddPlugin();
            plugin = plugins[0];
        }
    }

    static void AddPlugin()
    {
        SaveError = false;

        plugin = new Plugin();
        plugin.Number = "0";
        plugins.Add(plugin);
        plugins = PluginSort();

        selStrings = new string[plugins.Count];
        for (int i = 0; i < plugins.Count; i++)
            selStrings[i] = plugins[i].Number.ToString();
        selGridInt = 0;
    }

    public static List<Plugin> LoadData()
    {
        JsonLoader<Plugin> loader = new JsonLoader<Plugin>();
        return loader.LoadData();
    }

    public Plugin LoadData(string PluginNum)
    {
        JsonLoader<Plugin> loader = new JsonLoader<Plugin>();
        List<Plugin> pluginlist = new List<Plugin>();

        pluginlist = loader.LoadData();

        Plugin returnPlugin = new Plugin();
        for (int i = 0; i < pluginlist.Count; i++)
        {
            if (pluginlist[i].Number == PluginNum)
                returnPlugin = pluginlist[i];
        }

        return returnPlugin;
    }

    public void SaveData(Plugin plugin)
    {
        JsonLoader<Plugin> loader = new JsonLoader<Plugin>();
        List<Plugin> pluginlist = new List<Plugin>();

        pluginlist = LoadData();

        int index = 0, i;
        for (i = 0; i < pluginlist.Count; i++)
        {
            if (pluginlist[i].Number == plugin.Number)
                break;
        }
        index = i;
        if (index != pluginlist.Count)
        {
            pluginlist.RemoveAt(index);
        }
        pluginlist.Insert(index, plugin);

        loader.SaveData(pluginlist);

        selStrings = new string[plugins.Count];
        for (int ii = 0; ii < plugins.Count; ii++)
            selStrings[ii] = plugins[ii].Number.ToString();
    }
    public void DeleteData(int index, List<Plugin> pluginlist)
    {
        SaveError = false;

        SaveData(plugin);

        SaveError = false;

        pluginlist.RemoveAt(index);
        JsonLoader<Plugin> loader = new JsonLoader<Plugin>();

        //pluginlist = PluginSort(pluginlist);

        loader.SaveData(pluginlist);

        plugins = LoadData();
        selStrings = new string[plugins.Count];
        for (int i = 0; i < plugins.Count; i++)
            selStrings[i] = plugins[i].Number.ToString();
        selGridInt = 0;
        plugin = plugins[selGridInt];
    }
    public static List<Plugin> PluginSort()
    {
        if (plugins.Count > 1)
            plugins.Sort();
        return plugins;
    }
}
#endif