#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.UIElements;

public class WingmanMenu : EditorWindow
{
    static List<Wingman> wingmans;
    static Wingman wingman = new Wingman();
    static string[] selStrings;

    static int selGridInt = 0;
    static int selGridIntTemp = 0;
    private bool SaveError = false;
    private bool SaveErrorTemp = false;
    [MenuItem("Nova/WingmanEditor", false, 0)]

    static void Init()
    {
        WingmanMenu wingmanMenu = new WingmanMenu();
        wingmanMenu.InitValue();
        WingmanMenu window = (WingmanMenu)GetWindow(typeof(WingmanMenu));
        window.Show();
    }
    public void InitValue()
    {
        WingmanJsonLoader loader = new WingmanJsonLoader();
        wingmans = loader.LoadData();
        selStrings = new string[wingmans.Count];
        for (int i = 0; i < wingmans.Count; i++)
        {
            selStrings[i] = wingmans[i].Number;
        }
        if (wingmans.Count >= 1)
        {
            wingman = wingmans[0];
        }
        else
        {
            AddWingman();
            wingman = wingmans[0];
        }
    }
    private void OnGUI()
    {
        GUILayout.Label("僚机参数", EditorStyles.largeLabel);
        wingman.Number = EditorGUILayout.TextField("僚机编号", wingman.Number);
        wingman.Name = EditorGUILayout.TextField("僚机名称", wingman.Name);
        wingman.WeaponNumber = EditorGUILayout.TextField("所用武器编号", wingman.WeaponNumber);
        wingman.Price = EditorGUILayout.IntField("僚机价格", wingman.Price);
        GUILayout.Label("僚机描述");
        wingman.Description = EditorGUILayout.TextArea(wingman.Description);
        GUILayout.Space(10);

        GUILayout.Label("稀有度：" + wingman.rareLevel.ToString());
        if (GUILayout.Button("稀有度向上"))
        {
            wingman.rareLevel++;
        }
        if (GUILayout.Button("稀有度向下"))
        {
            wingman.rareLevel--;
        }
        GUILayout.Space(10);
        if (GUILayout.Button("新建空僚机"))
        {
            AddWingman();
        }
        if (GUILayout.Button("删除该僚机"))
        {
            DeleteWingman();
        }
        if (GUILayout.Button("保存此修改"))
        {
            SaveWingman();
        }
        GUILayout.Label("僚机列表", EditorStyles.largeLabel);
        GUILayout.BeginVertical("Box");
        selGridIntTemp = selGridInt;
        selGridInt = GUILayout.SelectionGrid(selGridInt, selStrings, 1);
        if (selGridInt != selGridIntTemp)
        {
            wingman = wingmans[selGridInt];
        }
        GUILayout.EndVertical();
        GUILayout.Space(10);
    }
    /// <summary>
    /// 按wingmans刷新展示列表
    /// </summary>
    private void UpadteGUI()        
    {
        selStrings = new string[wingmans.Count];
        for (int i = 0; i < wingmans.Count; i++)
            selStrings[i] = wingmans[i].Number;
        wingman = wingmans[0];      //刷新后选中列表第一个僚机
        selGridInt = 0;
    }
    /// <summary>
    /// 触发保存修改时所用方法
    /// </summary>
    private void SaveWingman()
    {
        SaveErrorTemp = false;
        for (int i = 0; i < wingmans.Count; i++)
        {
            if (wingmans[i].Number == wingman.Number && i != selGridInt)
            {
                SaveErrorTemp = true;
            }
        }
        if (wingman.Number == null || wingman.Number == "")
        {
            SaveErrorTemp = true;
        }
        SaveError = SaveErrorTemp;

        if (!SaveErrorTemp)
        {
            WingmanJsonLoader loader = new WingmanJsonLoader();
            loader.SaveData(wingman);       //将当前wingman存入json并排序
            wingmans = loader.LoadData();   //读取json中已排序的信息
            UpadteGUI();
        }
    }
    /// <summary>
    /// 触发新建僚机按钮时所用方法，
    /// </summary>
    private void AddWingman()
    {
        SaveError = false;
        WingmanJsonLoader loader = new WingmanJsonLoader();
        wingman = new Wingman();        //新建一个空白僚机并*暂存*于wingmans列表
        wingman.Number = "default";
        wingman.Name = "default";
        wingman.WeaponNumber = "default";
        wingman.Price = 0;
        wingman.rareLevel = 0;
        wingmans.Add(wingman);
        UpadteGUI();
    }
    /// <summary>
    /// 触发删除按钮时所用方法，当前选中的僚机将会被删除
    /// </summary>
    private void DeleteWingman()
    {        
        WingmanJsonLoader loader = new WingmanJsonLoader();
        loader.DeleteData(selGridInt,wingmans);
        wingmans = loader.LoadData();   //读取json中已排序的信息
        UpadteGUI();
    }

}
#endif