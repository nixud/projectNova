using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponNormalGun : WeaponNew
{
    Weapon weapon;
    GameObject bullet;

    bool IsInCD;

    public override void Shoot(Vector3 shootPosition, Vector3 shootForward)
    {
        if (!IsInCD)
        {
            if (!weapon.IsAShotgun)
            {
                bullet = ObjectPool.GetInstance().GetObj(weapon.BulletNumber, "Bullets");
                bullet.GetComponent<Bullet>().dir = shootForward;
                bullet.GetComponent<Bullet>().transform.position = shootPosition;
            }
        }

        StartCoroutine(WeaponCD());
    }

    IEnumerator WeaponCD()
    {
        IsInCD = true;
        yield return new WaitForSeconds(weapon.FireSpeed);
        IsInCD = false;
    }
}
