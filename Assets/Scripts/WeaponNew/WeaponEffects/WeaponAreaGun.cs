using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAreaGun : WeaponNew
{
    GameObject razer;

    private string RayNumber;
    private float RayDamagePerSec;
    private float HittedTimePerSec;
    private float RazerWidth;
    private float AreaRadius;

    public override void LoadInfomation(WeaponInformation weapon)
    {
        Number = weapon.Number;
        WeaponName = weapon.WeaponName;
        Description = weapon.Description;
        RayNumber = weapon.WeaponStrings[0];
        RayDamagePerSec = weapon.WeaponFloats[0];
        HittedTimePerSec = weapon.WeaponFloats[1];
        RazerWidth = weapon.WeaponFloats[2];
        AreaRadius = weapon.WeaponFloats[3];

        IconPath = weapon.IconPath;
        PicPath = weapon.PicPath;

        rareLevel = weapon.rareLevel;
    }

    public override void Shoot(Vector3 shootPosition, Vector3 shootForward)
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, AreaRadius);
        for (int i = 0; cols.Length > 0 && i < cols.Length; i++)
        {
            if (cols[i].gameObject.CompareTag("Enemy"))
            {
                if (!IsInCD)
                {
                    cols[i].gameObject.GetComponent<EnemyControl>().Hitted(RayDamagePerSec / HittedTimePerSec);
                }
                razer = ObjectPool.GetInstance().GetObj(RayNumber, "Rays");
                razer.GetComponent<LineRenderer>().startWidth = RazerWidth;
                razer.GetComponent<LineRenderer>().endWidth = RazerWidth;
                razer.GetComponent<LineRenderer>().SetPosition(0, transform.position);
                razer.GetComponent<LineRenderer>().SetPosition(1, cols[i].transform.position);
                StartCoroutine(RazerRecycle(razer));
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
        yield return new WaitForSeconds(1f/HittedTimePerSec);
        IsInCD = false;
    }
    /// <summary>
    /// 当时实现以为是不停闪烁的射线对范围内攻击，了解后是跟随攻击，故将waitforseconds改成waitforendofframe
    /// </summary>
    /// <param name="r"></param>
    /// <returns></returns>
    IEnumerator RazerRecycle(GameObject r)
    {
        yield return new WaitForEndOfFrame();
        ObjectPool.GetInstance().RecycleObj(r);
    }
}
