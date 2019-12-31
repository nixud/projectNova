#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WaveMenu : EditorWindow
{
    static List<Wave> waves;

    static Wave wave;

    static string[] selStrings;

    static bool SaveError = false;
    //static bool SaveErrorTemp = false;
    //static bool NoWeapon = false;

    static int selGridInt = 0;
    static int selGridInttemp = 0;

    [MenuItem("Nova/WaveEditor", false, 0)]
    static void Init()
    {
        WaveMenu WaveMenu = new WaveMenu();
        InitValue();
        WaveMenu window = (WaveMenu)GetWindow(typeof(WaveMenu));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("波次参数", EditorStyles.largeLabel);
        wave.Number = EditorGUILayout.TextField("波次编号", wave.Number);

        wave.EnemyNumber = EditorGUILayout.TextField("敌人编号", wave.EnemyNumber);

        wave.EnemyNum = EditorGUILayout.IntField("敌人数量",wave.EnemyNum);

        wave.Diffculty = EditorGUILayout.IntField("波次难度", wave.Diffculty);

        wave.MapNum = EditorGUILayout.IntField("星系编号", wave.MapNum);

        GUILayout.BeginVertical("Box");
        selGridInttemp = selGridInt;
        selGridInt = GUILayout.SelectionGrid(selGridInt, selStrings, 1);
        if (selGridInt != selGridInttemp)
        {
            wave = waves[selGridInt];
        }
        GUILayout.EndVertical();

        if (SaveError) EditorGUILayout.HelpBox("输入内容不正确。请检查编号是否重复，编号是否为空等", MessageType.Error);

        if (GUILayout.Button("添加新波次"))
        {
            AddWave();
        }
        if (GUILayout.Button("保存波次信息"))
        {
            SaveData(wave);
        }
        if (GUILayout.Button("删除波次信息"))
        {
            DeleteData(selGridInt, waves);
        }
    }

    public static void InitValue()
    {
        waves = LoadData();
        selStrings = new string[waves.Count];
        for (int i = 0; i < waves.Count; i++)
            selStrings[i] = waves[i].Number.ToString();
        if (waves.Count >= 1)
            wave = waves[0];
        else
        {
            AddWave();
            wave = waves[0];
        }
    }

    static void AddWave()
    {
        SaveError = false;

        wave = new Wave();
        wave.Number = "0";
        waves.Add(wave);
        waves = WaveSort();

        selStrings = new string[waves.Count];
        for (int i = 0; i < waves.Count; i++)
            selStrings[i] = waves[i].Number.ToString();
        selGridInt = 0;
    }

    public static List<Wave> LoadData()
    {
        JsonLoader<Wave> loader = new JsonLoader<Wave>();
        return loader.LoadData();
    }

    public Wave LoadData(string WaveNum)
    {
        JsonLoader<Wave> loader = new JsonLoader<Wave>();
        List<Wave> wavelist = new List<Wave>();

        wavelist = loader.LoadData();

        Wave returnWave = new Wave();
        for (int i = 0; i < wavelist.Count; i++)
        {
            if (wavelist[i].Number == WaveNum)
                returnWave = wavelist[i];
        }

        return returnWave;
    }

    public void SaveData(Wave wave)
    {
        JsonLoader<Wave> loader = new JsonLoader<Wave>();
        List<Wave> wavelist = new List<Wave>();

        wavelist = LoadData();

        int index = 0, i;
        for (i = 0; i < wavelist.Count; i++)
        {
            if (wavelist[i].Number == wave.Number)
                break;
        }
        index = i;
        if (index != wavelist.Count)
        {
            wavelist.RemoveAt(index);
        }
        wavelist.Insert(index, wave);

        loader.SaveData(wavelist);

        selStrings = new string[waves.Count];
        for (int ii = 0; ii < waves.Count; ii++)
            selStrings[ii] = waves[ii].Number.ToString();
    }
    public void DeleteData(int index, List<Wave> wavelist)
    {
        SaveError = false;

        SaveData(wave);

        SaveError = false;

        wavelist.RemoveAt(index);
        JsonLoader<Wave> loader = new JsonLoader<Wave>();

        //wavelist = WaveSort(wavelist);

        loader.SaveData(wavelist);

        waves = LoadData();
        selStrings = new string[waves.Count];
        for (int i = 0; i < waves.Count; i++)
            selStrings[i] = waves[i].Number.ToString();
        selGridInt = 0;
        wave = waves[selGridInt];
    }
    public static List<Wave> WaveSort()
    {
        if (waves.Count > 1)
            waves.Sort();
        return waves;
    }
}
#endif