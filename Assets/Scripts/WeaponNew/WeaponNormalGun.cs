using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponNormalGun : WeaponNew
{
    GameObject bullet;

    bool IsInCD;

    public WeaponNormalGun(Weapon weapon) {
        Number = weapon.Number;
        WeaponName = weapon.WeaponName;
        Description = weapon.Description;
        FireSpeed = weapon.FireSpeed;
        BulletNumber = weapon.BulletNumber;

        IconPath = weapon.IconPath;
        PicPath = weapon.PicPath;

        rareLevel = weapon.rareLevel;
    }

    public override void Shoot(Vector3 shootPosition, Vector3 shootForward)
    {
        if (!IsInCD)
        {
            if (!IsAShotgun)
            {
                bullet = ObjectPool.GetInstance().GetObj(BulletNumber, "Bullets");
                bullet.GetComponent<Bullet>().dir = shootForward;
                bullet.GetComponent<Bullet>().transform.position = shootPosition;
            }
        }

        StartCoroutine(WeaponCD());
    }

    IEnumerator WeaponCD()
    {
        IsInCD = true;
        yield return new WaitForSeconds(FireSpeed);
        IsInCD = false;
    }
}
