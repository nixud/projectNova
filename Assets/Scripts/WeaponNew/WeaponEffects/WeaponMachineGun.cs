using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

[Hotfix]
public class WeaponMechineGun : WeaponNew
{
    GameObject bullet;

    private bool IsInShootCD = false;
    private int NowBullet = 0;//目前已经发射出去的子弹量

    private float ShootWaitTime;//射击间的等待时间
    private float BulletWaitTime;//每次子弹的时间
    private string BulletNumber;
    private int BulletTotalNumber;//一次射击要发射多少枚子弹

    public override void LoadInfomation(WeaponInformation weapon) {
        Number = weapon.Number;
        WeaponName = weapon.WeaponName;
        Description = weapon.Description;

        ShootWaitTime = weapon.WeaponFloats[0];
        BulletWaitTime = weapon.WeaponFloats[1];
        BulletNumber = weapon.WeaponStrings[0];
        BulletTotalNumber = weapon.WeaponInts[0];

        IconPath = weapon.IconPath;
        PicPath = weapon.PicPath;

        rareLevel = weapon.rareLevel;
    }

    public override void Shoot(Vector3 shootPosition, Vector3 shootForward)
    {
        if (!IsInCD)
        {
            if (!IsInShootCD)
            {
                NowBullet++;

                bullet = ObjectPool.GetInstance().GetObj(BulletNumber, "Bullets");
                bullet.GetComponent<Bullet>().dir = shootForward;
                bullet.GetComponent<Bullet>().transform.position = shootPosition;
                StartCoroutine(ShootCD());
            }

            if (NowBullet >= BulletTotalNumber)
            {
                StartCoroutine(WeaponCD());
                NowBullet = 0;
            }
        }
    }

    IEnumerator WeaponCD()
    {
        IsInCD = true;
        yield return new WaitForSeconds(ShootWaitTime);
        IsInCD = false;
    }

    IEnumerator ShootCD() {
        IsInShootCD = true;
        yield return new WaitForSeconds(BulletWaitTime);
        IsInShootCD = false;
    }
}
