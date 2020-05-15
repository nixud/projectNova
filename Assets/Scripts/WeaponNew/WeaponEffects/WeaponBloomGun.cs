using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBloomGun : WeaponNew
{
    GameObject bullet;

    private float FireSpeed;
    private float CDTime;
    private float RotateSpeed;
    private string BulletNumber;
    private int BulletForwardCount;
    private int BulletPerForward;
    private float Offset;
    private bool IsInShootingCD = false;

    private Vector3 ShootPos;

    public override void LoadInfomation(WeaponInformation weapon)
    {
        Number = weapon.Number;
        WeaponName = weapon.WeaponName;
        Description = weapon.Description;
        FireSpeed = weapon.WeaponFloats[0];
        RotateSpeed = weapon.WeaponFloats[1];
        CDTime = weapon.WeaponFloats[2];
        BulletNumber = weapon.WeaponStrings[0];
        BulletForwardCount = weapon.WeaponInts[0];
        BulletPerForward = weapon.WeaponInts[1];
        IconPath = weapon.IconPath;
        PicPath = weapon.PicPath;
        Offset = 0f;
        rareLevel = weapon.rareLevel;
    }

    public override void Shoot(Vector3 shootPosition, Vector3 shootForward)
    {
        if (!IsInCD)
        {
            ShootPos = shootPosition;
            IsInCD = true;
            StartCoroutine(WeaponCD());
        }

    }

    IEnumerator Shoot()
    {
        int BulletNum = BulletForwardCount;
        for (int i = 0; i < BulletNum; i++)
        {
            float angle = (Offset + i * (2 * Mathf.PI) / BulletNum);
            bullet = ObjectPool.GetInstance().GetObj(BulletNumber, "Bullets");
            Vector3 dir;
            dir = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
            dir.Normalize();
            bullet.GetComponent<BulletHelper>().bulletNew.transform.Rotate(new Vector3(0, 0, angle * 180 / Mathf.PI));
            bullet.GetComponent<BulletHelper>().bulletNew.transform.position = ShootPos;

            if (gameObject.CompareTag("Player"))
                gameObject.GetComponent<CharacterBulletControl>().AddBullet(bullet);
        }
        IsInShootingCD = true;
        yield return new WaitForSeconds(FireSpeed);
        IsInShootingCD = false;
    }

    IEnumerator WeaponCD()
    {
        for (int a = 0; a < BulletPerForward; a++)
        {
            yield return StartCoroutine(Shoot());
            if (RotateSpeed != 0f)
            {
                float RotateAngle = (RotateSpeed / 180) * Mathf.PI;
                Offset = (Offset + RotateAngle) % (2 * Mathf.PI);
            }
        }
        yield return new WaitForSeconds(CDTime);
        IsInCD = false;
    }
}

