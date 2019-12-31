#if UNITY_EDITOR
using UnityEngine;
using UnityEditor.Experimental.UIElements;
using UnityEditor;
using System;
using System.Collections.Generic;

public class WeaponMenu : EditorWindow
{
    //int number = 0;

    bool SaveError = false;
    bool SaveErrorTemp = false;
    //bool NoWeapon = false;

    int SpecialNumber;

    int selGridInt = 0;
    int selGridInttemp = 0;
    static string[] selStrings;
    static List<Weapon> weapons;

    static Weapon weapon;

    // Add menu item
    [MenuItem("Nova/WeaponEditor",false,0)]
    static void Init()
    {
        WeaponMenu weaponMenu = new WeaponMenu();
        weaponMenu.InitValue();
        WeaponMenu window = (WeaponMenu)GetWindow(typeof(WeaponMenu));
        window.Show();
    }

    public void InitValue() {
        WeaponJsonLoader jsonLoader = new WeaponJsonLoader();
        weapons = jsonLoader.LoadData();
        selStrings = new string[weapons.Count];
        for (int i = 0; i < weapons.Count; i++)
            selStrings[i] = weapons[i].Number;
        if (weapons.Count >= 1)
            weapon = weapons[0];
        else
        {
            AddWeapon();
            weapon = weapons[0];
        }
    }

    void OnGUI()
    {

        // The actual window code goes here
        GUILayout.Label("武器参数", EditorStyles.largeLabel);
        //test = EditorGUILayout.TextField("武器编号", test);
        weapon.Number = EditorGUILayout.TextField("武器编号", weapon.Number);
        weapon.WeaponName = EditorGUILayout.TextField("武器名称", weapon.WeaponName);
        GUILayout.Label("武器描述");
        weapon.Description = EditorGUILayout.TextArea(weapon.Description, GUILayout.MaxHeight(75));
        //EditorGUILayout.SelectableLabel("test");

        SpecialNumber = EditorGUILayout.IntField("特殊武器的编号", SpecialNumber);
        if (SpecialNumber != 0)
            EditorGUILayout.HelpBox("这部分武器的效果将会被专门实现", MessageType.Info);
        else
        {
            weapon.isRay = EditorGUILayout.Toggle("是否为激光武器", weapon.isRay);
            if (weapon.isRay)
            {
                weapon.RayNumber = EditorGUILayout.TextField("射线编号", weapon.RayNumber);
                weapon.RayDamagePerSec = EditorGUILayout.IntField("每秒伤害", weapon.RayDamagePerSec);
            }
            else
            {
                weapon.FireSpeed = EditorGUILayout.FloatField("武器射速", weapon.FireSpeed);
                weapon.BulletNumber = EditorGUILayout.TextField("子弹编号", weapon.BulletNumber);
                weapon.Accuracy = EditorGUILayout.FloatField("子弹散布程度（0-1）", weapon.Accuracy);
                if (weapon.IsAShotgun = EditorGUILayout.Toggle("是否为霰弹", weapon.IsAShotgun))
                {
                    if (weapon.IsShotGunEven = EditorGUILayout.Toggle("霰弹分布是否均匀", weapon.IsShotGunEven))
                        EditorGUILayout.HelpBox("霰弹将会根据子弹散布程度在0-180度内均匀分散", MessageType.Info);
                    else EditorGUILayout.HelpBox("霰弹将会根据子弹散布程度在0-180度内随机分散", MessageType.Info);
                    weapon.ShotgunNum = EditorGUILayout.IntField("一次发出的霰弹数量", weapon.ShotgunNum);
                    weapon.ShotgunRandomNum = EditorGUILayout.IntField("霰弹随机量，0为固定", weapon.ShotgunRandomNum);
                }
            }
        }


        GUILayout.Space(10);

        GUILayout.BeginVertical("Box");
        selGridInttemp = selGridInt;
        selGridInt = GUILayout.SelectionGrid(selGridInt, selStrings, 1);
        if(selGridInt!=selGridInttemp)
        {
            weapon = weapons[selGridInt];
        }
        GUILayout.EndVertical();

        GUILayout.Label("稀有度：" + weapon.rareLevel.ToString());
        if (GUILayout.Button("稀有度向上"))
        { weapon.rareLevel++; }
        if (GUILayout.Button("稀有度向下"))
        { weapon.rareLevel--; }

        GUILayout.Space(10);

        if (SaveError) EditorGUILayout.HelpBox("输入内容不正确。请检查编号是否重复，编号是否为空等", MessageType.Error);

        if (GUILayout.Button("添加新武器"))
        {
            AddWeapon();
        }
        if (GUILayout.Button("保存武器信息"))
        {
            SaveWeapon();
        }
        if (GUILayout.Button("删除武器信息"))
        {
            DeleteWeapon();
        }

    }

    void AddWeapon()
    {
        SaveError = false;
        WeaponJsonLoader jsonLoader = new WeaponJsonLoader();
        weapon = new Weapon();
        weapon.Number = "0";
        weapons.Add(weapon);
        weapons = jsonLoader.WeaponSort(weapons);

        selStrings = new string[weapons.Count];
        for (int i = 0; i < weapons.Count; i++)
            selStrings[i] = weapons[i].Number;
        selGridInt = 0;
    }

    void SaveWeapon() {
        SaveErrorTemp = false;
        for (int i=0;i<weapons.Count;i++) {
            if (weapons[i].Number == weapon.Number && i!=selGridInt) {
                SaveErrorTemp = true;
            }
        }
        if (weapon.Number == null || weapon.Number == "") {
            SaveErrorTemp = true;
        }
        SaveError = SaveErrorTemp;

        if (!SaveErrorTemp)
        {
            WeaponJsonLoader jsonLoader = new WeaponJsonLoader();
            jsonLoader.SaveData(weapon);

            selStrings = new string[weapons.Count];
            for (int i = 0; i < weapons.Count; i++)
                selStrings[i] = weapons[i].Number;
        }
    }

    void DeleteWeapon() {
        SaveError = false;

        SaveWeapon();

        SaveError = false;

        WeaponJsonLoader jsonLoader = new WeaponJsonLoader();
        jsonLoader.DeleteData(selGridInt,weapons);

        weapons = jsonLoader.LoadData();
        selStrings = new string[weapons.Count];
        for (int i = 0; i < weapons.Count; i++)
            selStrings[i] = weapons[i].Number;
        selGridInt = 0;
        weapon = weapons[selGridInt];
    }
}
#endif