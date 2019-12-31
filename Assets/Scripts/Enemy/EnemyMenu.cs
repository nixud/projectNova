#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemyMenu : EditorWindow
{
    static List<Enemy> enemies;

    static Enemy enemy = new Enemy();

    static string[] selStrings;

    //static bool SaveError = false;
    //static bool SaveErrorTemp = false;
    //static bool NoWeapon = false;

    static int selGridInt = 0;
    static int selGridInttemp = 0;

    [MenuItem("Nova/EnemyEditor", false, 0)]
    static void Init()
    {
        EnemyMenu enemyMenu = new EnemyMenu();
        EnemyMenu.InitValue();
        EnemyMenu window = (EnemyMenu)GetWindow(typeof(EnemyMenu));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("小怪参数", EditorStyles.largeLabel);
        enemy.Number = EditorGUILayout.TextField("小怪编号", enemy.Number);

        enemy.Prefab = EditorGUILayout.TextField("小怪预制体", enemy.Prefab);

        GUILayout.BeginVertical("Box");
        selGridInttemp = selGridInt;
        selGridInt = GUILayout.SelectionGrid(selGridInt, selStrings, 1);
        if (selGridInt != selGridInttemp)
        {
            enemy = enemies[selGridInt];
        }
        GUILayout.EndVertical();

        if (GUILayout.Button("添加新敌人"))
        {
            AddEnemy();
        }
        if (GUILayout.Button("保存敌人信息"))
        {
            SaveData(enemy);
        }
        if (GUILayout.Button("删除敌人信息"))
        {
            DeleteData(selGridInt, enemies);
        }
    }

    public static void InitValue()
    {
        enemies = LoadData();
        selStrings = new string[enemies.Count];
        for (int i = 0; i < enemies.Count; i++)
            selStrings[i] = enemies[i].Number.ToString();
        if (enemies.Count >= 1)
            enemy = enemies[0];
        else
        {
            AddEnemy();
            enemy = enemies[0];
        }
    }
    static void AddEnemy()
    {
        //SaveError = false;

        enemy = new Enemy();
        enemy.Number = "";
        enemies.Add(enemy);
        //enemies = EnemySort();

        selStrings = new string[enemies.Count];
        for (int i = 0; i < enemies.Count; i++)
            selStrings[i] = enemies[i].Number.ToString();
        selGridInt = 0;
    }

    public static List<Enemy> LoadData()
    {
        JsonLoader<Enemy> loader = new JsonLoader<Enemy>();
        return loader.LoadData();
    }

    public Enemy LoadData(string EnemyNum)
    {
        JsonLoader<Enemy> loader = new JsonLoader<Enemy>();
        List<Enemy> enemylist = new List<Enemy>();

        enemylist = loader.LoadData();

        Enemy returnEnemy = new Enemy();
        for (int i = 0; i < enemylist.Count; i++)
        {
            if (enemylist[i].Number == EnemyNum)
                returnEnemy = enemylist[i];
        }

        return returnEnemy;
    }

    public void SaveData(Enemy enemy)
    {
        JsonLoader<Enemy> loader = new JsonLoader<Enemy>();
        List<Enemy> enemylist = new List<Enemy>();

        enemylist = LoadData();

        int index = 0, i;
        for (i = 0; i < enemylist.Count; i++)
        {
            if (enemylist[i].Number == enemy.Number)
                break;
        }
        index = i;
        if (index != enemylist.Count)
        {
            enemylist.RemoveAt(index);
        }
        enemylist.Insert(index, enemy);

        loader.SaveData(enemylist);

        selStrings = new string[enemies.Count];
        for (int ii = 0; ii < enemies.Count; ii++)
            selStrings[ii] = enemies[ii].Number.ToString();
    }
    public void DeleteData(int index, List<Enemy> enemylist)
    {
        //SaveError = false;

        SaveData(enemy);

        //SaveError = false;

        enemylist.RemoveAt(index);
        JsonLoader<Enemy> loader = new JsonLoader<Enemy>();

        //enemylist = EnemySort(enemylist);

        loader.SaveData(enemylist);

        enemies = LoadData();
        selStrings = new string[enemies.Count];
        for (int i = 0; i < enemies.Count; i++)
            selStrings[i] = enemies[i].Number.ToString();
        selGridInt = 0;
        enemy = enemies[selGridInt];
    }
}
#endif