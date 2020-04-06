using System;
using XLua;
using UnityEngine;

[Hotfix]
public abstract class WeaponNew : IComparable
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

    public int CompareTo(object obj)
    {
        Weapon p = obj as Weapon;
        return CompareTo(p.Number);
    }

    public abstract void Shoot(Vector3 shootPosition, Vector3 shootForward);
}