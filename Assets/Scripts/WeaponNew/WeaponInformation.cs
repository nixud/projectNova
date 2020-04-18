using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponInformation : IComparable
{
    public string Number;
    public string WeaponName;
    public string Description = "没有描述";

    public int WeaponSort;//武器的种类
    public List<int> WeaponInts;
    public List<string> WeaponStrings;
    public List<bool> WeaponBools;
    public List<float> WeaponFloats;

    public string IconPath;
    public string PicPath;

    public RareLevel rareLevel = 0;

    public int CompareTo(object obj) {
        WeaponInformation p = obj as WeaponInformation;
        return CompareTo(p.Number);
    }
}