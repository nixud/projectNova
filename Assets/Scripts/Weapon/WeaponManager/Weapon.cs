using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Weapon : IComparable
{
    public string Number;
    public string WeaponName;
    public string Description = "没有描述";
    public float FireSpeed;
    public bool isRay;//是否为激光
    public string RayNumber;
    public string BulletNumber;
    public float Accuracy;//0-1
    public bool IsAShotgun;
    public bool IsShotGunEven;
    public int ShotgunNum;
    public int ShotgunRandomNum;
    public int SpecialNumber;

    public int RayDamagePerSec;

    public string IconPath;
    public string PicPath;

    public RareLevel rareLevel = 0;

    public int CompareTo(object obj) {
        Weapon p = obj as Weapon;
        return this.Number.CompareTo(p.Number);
    }
}