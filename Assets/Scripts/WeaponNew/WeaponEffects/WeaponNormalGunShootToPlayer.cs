using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

[Hotfix]
public class WeaponNormalGunShootToPlayer : WeaponNew
{
    GameObject bullet;

    private float FireSpeed;
    private string BulletNumber;

    public override void LoadInfomation(WeaponInformation weapon) {
        Number = weapon.Number;
        WeaponName = weapon.WeaponName;
        Description = weapon.Description;

        FireSpeed = weapon.WeaponFloats[0];
        BulletNumber = weapon.WeaponStrings[0];

        IconPath = weapon.IconPath;
        PicPath = weapon.PicPath;

        rareLevel = weapon.rareLevel;
    }

    public override void Shoot(Vector3 shootPosition, Vector3 shootForward)
    {
        if (!IsInCD)
        {

            bullet = ObjectPool.GetInstance().GetObj(BulletNumber, "Bullets");

            bullet.GetComponent<BulletHelper>().bulletNew.dir = shootForward;
            bullet.GetComponent<BulletHelper>().bulletNew.transform.position = shootPosition;
            
            // 添加子弹到角色子弹管理
            if (gameObject.CompareTag("Player"))
                gameObject.GetComponent<CharacterBulletControl>().AddBullet(bullet);

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
