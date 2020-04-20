using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLight : WeaponNew
{

    GameObject ray;

    public string RayNumber;
    public float RayDamagePerSec;
    public float HittedTimePerSec;
    public float RayLength;
    public float RayWidth;

    Vector3 shootPosition;
    Vector3 shootForward;

    public override void LoadInfomation(WeaponInformation weapon)
    {
        Number = weapon.Number;
        WeaponName = weapon.WeaponName;
        Description = weapon.Description;

        RayNumber = weapon.WeaponStrings[0];
        RayDamagePerSec = weapon.WeaponFloats[0];
        HittedTimePerSec = weapon.WeaponFloats[1];
        RayLength = weapon.WeaponFloats[2];
        RayWidth = weapon.WeaponFloats[3];

        IconPath = weapon.IconPath;
        PicPath = weapon.PicPath;

        rareLevel = weapon.rareLevel;

        ray = ObjectPool.GetInstance().GetObj(RayNumber, "Rays");

        ray.GetComponent<LineRenderer>().startWidth = RayWidth;
        ray.GetComponent<LineRenderer>().endWidth = RayWidth;
    }

    public override void Shoot(Vector3 shootPosition, Vector3 shootForward)
    {
        this.shootPosition = shootPosition;
        this.shootForward = shootForward;
        ray.GetComponent<LineRenderer>().SetPosition(0, shootPosition);
        ray.GetComponent<LineRenderer>().SetPosition(1, shootPosition + shootForward.normalized * RayLength);
    }

    private void Update()
    {
        Attack();
    }
    private void Attack()
    {
        RaycastHit2D[] hits = Physics2D.LinecastAll(transform.position,shootForward*RayLength);
        
        for (int i = 0; hits.Length > 0 && i < hits.Length; i++)
        {
            if (hits[i].collider.gameObject.CompareTag("Enemy"))
            {
                if (!IsInCD)
                {
                    
                    hits[i].collider.gameObject.GetComponent<EnemyControl>().Hitted(RayDamagePerSec / HittedTimePerSec);
                }

            }
        }
        if (!IsInCD)
        {
            StartCoroutine(WeaponCD());
        }
    }
    IEnumerator WeaponCD()
    {
        IsInCD = true;
        yield return new WaitForSeconds(1f / HittedTimePerSec);
        IsInCD = false;
    }
}
