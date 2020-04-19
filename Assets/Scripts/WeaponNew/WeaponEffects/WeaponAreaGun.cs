using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAreaGun : WeaponNew
{
    GameObject razer;

    private string RayNumber;
    private float RayDamagePerSec;
    private float HittedTimePerSec;
    private float RayWidth;
    private float AreaRadius;

    public override void LoadInfomation(WeaponInformation weapon)
    {
        Number = weapon.Number;
        WeaponName = weapon.WeaponName;
        Description = weapon.Description;
        RayNumber = weapon.WeaponStrings[0];
        RayDamagePerSec = weapon.WeaponFloats[0];
        HittedTimePerSec = weapon.WeaponFloats[1];
        RayWidth = weapon.WeaponFloats[2];
        AreaRadius = weapon.WeaponFloats[3];

        IconPath = weapon.IconPath;
        PicPath = weapon.PicPath;

        rareLevel = weapon.rareLevel;
    }

    public override void Shoot(Vector3 shootPosition, Vector3 shootForward)
    {
        if (!IsInCD)
        {
            Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, AreaRadius);
            for (int i = 0; cols.Length > 0 && i < cols.Length; i++)
            {
                if (cols[i].gameObject.CompareTag("Enemy"))
                {
                    StartCoroutine(RazerHitted(cols[i].gameObject));
                    razer = ObjectPool.GetInstance().GetObj(RayNumber, "Rays");
                    razer.GetComponent<LineRenderer>().startWidth = RayWidth;
                    razer.GetComponent<LineRenderer>().endWidth = RayWidth;
                    razer.GetComponent<LineRenderer>().SetPosition(0, transform.position);
                    razer.GetComponent<LineRenderer>().SetPosition(1, cols[i].transform.position);
                    StartCoroutine(RazerRecycle(razer));
                }
            }
            StartCoroutine(WeaponCD());
        }
    }

    IEnumerator RazerHitted(GameObject hittedObject)
    {
        for (int i = 0;i < HittedTimePerSec; i++)
        {
            hittedObject.GetComponent<EnemyControl>().Hitted(RayDamagePerSec/HittedTimePerSec);
            yield return new WaitForSeconds(1f / HittedTimePerSec);
        }
    }

    IEnumerator WeaponCD()
    {
        IsInCD = true;
        yield return new WaitForSeconds(1f);
        IsInCD = false;
    }

    IEnumerator RazerRecycle(GameObject r)
    {
        yield return new WaitForSeconds(0.3f);
        ObjectPool.GetInstance().RecycleObj(r);
    }
}
