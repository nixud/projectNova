﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

[Hotfix]
public class WeaponShotGun : WeaponNew
{
    GameObject bullet;

    public float FireSpeed;
    private string BulletNumber;

    private float Accuracy;
    private bool IsShotGunEven;
    private int ShotgunNum;
    private int ShotgunRandomNum;

    public override void LoadInfomation(WeaponInformation weapon) {
        Number = weapon.Number;
        WeaponName = weapon.WeaponName;
        Description = weapon.Description;

        FireSpeed = weapon.WeaponFloats[0];
        BulletNumber = weapon.WeaponStrings[0];

        Accuracy = weapon.WeaponFloats[1];
        IsShotGunEven = weapon.WeaponBools[0];
        ShotgunNum = weapon.WeaponInts[0];
        ShotgunRandomNum = weapon.WeaponInts[1];

        IconPath = weapon.IconPath;
        PicPath = weapon.PicPath;

        rareLevel = weapon.rareLevel;
    }

    public override void Shoot(Vector3 shootPosition, Vector3 shootForward)
    {
        if (!IsInCD)
        {
            float maxdegree = Accuracy * Mathf.PI / 2;
            int BulletNum = ShotgunNum;
            for (int i = 0; i < BulletNum; i++)
            {
                float angle = -maxdegree + i * (2 * maxdegree) / (BulletNum - 1);

                bullet = ObjectPool.GetInstance().GetObj(BulletNumber, "Bullets");
                Vector3 dir;
                dir = new Vector3(shootForward.y * Mathf.Sin(angle), shootForward.y * Mathf.Cos(angle), 0);
                dir.Normalize();
                bullet.GetComponent<BulletHelper>().bulletNew.dir = dir;
                bullet.GetComponent<BulletHelper>().bulletNew.transform.position = shootPosition;
                //Debug.Log(Mathf.Atan(dir.x / dir.y));
                float eulers = -Mathf.Atan(dir.x / dir.y) * 180 / Mathf.PI;
                bullet.GetComponent<BulletHelper>().bulletNew.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                bullet.GetComponent<BulletHelper>().bulletNew.transform.Rotate(new Vector3(0, 0, eulers));
                bullet.GetComponent<BulletHelper>().bulletNew.eulers = eulers;
                
                // 添加子弹到角色子弹管
                if (gameObject.CompareTag("Player"))
                    gameObject.GetComponent<CharacterBulletControl>().AddBullet(bullet);
            }

            StartCoroutine(WeaponCD());
        }
    }

    IEnumerator WeaponCD()
    {
        IsInCD = true;
        yield return new WaitForSeconds(FireSpeed);
        IsInCD = false;
    }
}
