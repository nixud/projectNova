using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

[Hotfix]
public class WeaponShotGun : WeaponNew
{
    GameObject bullet;

    public override void LoadInfomation(Weapon weapon) {
        Number = weapon.Number;
        WeaponName = weapon.WeaponName;
        Description = weapon.Description;
        FireSpeed = weapon.FireSpeed;
        BulletNumber = weapon.BulletNumber;

        Accuracy = weapon.Accuracy;
        IsAShotgun = weapon.IsAShotgun;
        IsShotGunEven = weapon.IsShotGunEven;
        ShotgunNum = weapon.ShotgunNum;
        ShotgunRandomNum = weapon.ShotgunRandomNum;

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

                bullet = ObjectPool.GetInstance().GetObj(this.BulletNumber, "Bullets");
                Vector3 dir = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0);
                dir.Normalize();
                bullet.GetComponent<Bullet>().dir = dir;
                bullet.GetComponent<Bullet>().transform.position = shootPosition;
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
