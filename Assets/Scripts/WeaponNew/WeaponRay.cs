﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

[Hotfix]
public class WeaponRay : WeaponNew
{
    float ShootWaitingTime = -1f;

    GameObject ray;

    bool IsRayhiting;
    bool IsRayOn;

    Vector3 shootPosition;
    Vector3 shootForward;

    public override void LoadInfomation(Weapon weapon) {
        Number = weapon.Number;
        WeaponName = weapon.WeaponName;
        Description = weapon.Description;

        isRay = weapon.isRay;
        RayNumber = weapon.RayNumber;
        RayDamagePerSec = weapon.RayDamagePerSec;

        IconPath = weapon.IconPath;
        PicPath = weapon.PicPath;

        rareLevel = weapon.rareLevel;

        if (weapon.isRay)
        {
            ray = ObjectPool.GetInstance().GetObj(weapon.RayNumber, "Rays");
        }

        ray.GetComponent<LineRenderer>().startWidth = 0.2f;
        ray.GetComponent<LineRenderer>().endWidth = 0.2f;
    }

    public override void Shoot(Vector3 shootPosition, Vector3 shootForward)
    {

        this.shootPosition = shootPosition;
        this.shootForward = shootForward;

        ShootWaitingTime = 0.05f;
    }

    private void Update() {

        if (ShootWaitingTime > 0)
        {
            ray.GetComponent<LineRenderer>().SetPosition(0, shootPosition);

            RaycastHit2D hit = Physics2D.Raycast(shootPosition, shootForward);

            if (hit.collider != null && hit.distance <= 30)
            {
                ray.GetComponent<LineRenderer>().SetPosition(1, hit.point);
                if (!IsRayhiting)
                    StartCoroutine(RayCD(hit));
            }
            else
            {
                ray.GetComponent<LineRenderer>().SetPosition(1, shootPosition + new Vector3(0, 20, 0));
            }
            ShootWaitingTime -= Time.deltaTime;
        }

    }

    IEnumerator RayCD(RaycastHit2D hit)
    {
        IsRayhiting = true;
        try
        {
            hit.collider.gameObject.GetComponent<EnemyControl>().Hitted(RayDamagePerSec / 5);
        }
        catch { }
        yield return new WaitForSeconds(0.2f);
        IsRayhiting = false;
    }
}
