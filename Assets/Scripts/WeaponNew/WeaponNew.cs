using System;
using System.Collections.Generic;
using XLua;
using UnityEngine;

[Hotfix]
public abstract class WeaponNew : MonoBehaviour, IComparable
{
    public string Number;
    public string WeaponName;
    public string Description = "没有描述";

    public string IconPath;
    public string PicPath;

    public RareLevel rareLevel = 0;

    protected bool IsInCD = false;

    public int CompareTo(object obj)
    {
        WeaponNew p = obj as WeaponNew;
        return CompareTo(p.Number);
    }

    public abstract void Shoot(Vector3 shootPosition, Vector3 shootForward);
    public abstract void LoadInfomation(WeaponInformation weapon);
}