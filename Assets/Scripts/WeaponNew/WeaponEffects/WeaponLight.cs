using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLight : WeaponNew
{
    GameObject AimingLine;
    GameObject Light;

    public string AimingLineName;
    public string LightName;
    public float LightDamagePerSec;
    public float HittedTimePerSec;
    public float LightLength;
    public float LightWidth;
    public float AimingTime;
    public float AttackingTime;
    public bool IsPlayerWeapon;
    bool IsAiming;
    bool IsAttacking;
    Vector3 shootPosition;
    Vector3 shootForward;

    public override void LoadInfomation(WeaponInformation weapon)
    {
        Number = weapon.Number;
        WeaponName = weapon.WeaponName;
        Description = weapon.Description;

        AimingLineName = weapon.WeaponStrings[0];
        LightName = weapon.WeaponStrings[1];
        LightDamagePerSec = weapon.WeaponFloats[0];
        HittedTimePerSec = weapon.WeaponFloats[1];
        LightLength = weapon.WeaponFloats[2];
        LightWidth = weapon.WeaponFloats[3];
        AimingTime = weapon.WeaponFloats[4];
        AttackingTime = weapon.WeaponFloats[5];
        IsPlayerWeapon = weapon.WeaponBools[0];

        IconPath = weapon.IconPath;
        PicPath = weapon.PicPath;

        rareLevel = weapon.rareLevel;

        IsAiming = false;
        IsAttacking = false;
    }

    public override void Shoot(Vector3 attackPosition, Vector3 attackForward)
    {
        shootPosition = attackPosition;
        shootForward = attackForward;
        if (!IsAiming&&!IsAttacking)
        {
            StartCoroutine(Aiming());
        }
    }

    private void Update()
    {
        Attack();
    }
    private void Attack()
    {
        if (IsAiming&&!IsAttacking)
        {
            AimingLine.GetComponent<LineRenderer>().startWidth = LightWidth;
            AimingLine.GetComponent<LineRenderer>().endWidth = LightWidth;
            AimingLine.GetComponent<LineRenderer>().SetPosition(0, shootPosition);
            AimingLine.GetComponent<LineRenderer>().SetPosition(1, shootPosition + shootForward.normalized * LightLength);
        }
        if (IsAttacking&&!IsAiming)
        {
            Light.GetComponent<LineRenderer>().startWidth = LightWidth;
            Light.GetComponent<LineRenderer>().endWidth = LightWidth;
            Light.GetComponent<LineRenderer>().SetPosition(0, shootPosition);
            Light.GetComponent<LineRenderer>().SetPosition(1, shootPosition + shootForward.normalized * LightLength);

            RaycastHit2D[] hits = Physics2D.LinecastAll(transform.position, shootForward * LightLength);
            for (int i = 0; hits.Length > 0 && i < hits.Length; i++)
            {
                if (IsPlayerWeapon)
                {
                    if (hits[i].collider.gameObject.CompareTag("Enemy"))
                    {
                        if (!IsInCD)
                        {

                            hits[i].collider.gameObject.GetComponent<EnemyControl>().Hitted(LightDamagePerSec / HittedTimePerSec);
                        }

                    }
                }
                else
                {
                    if (hits[i].collider.gameObject.CompareTag("Player"))
                    {
                        if (!IsInCD)
                        {
                            hits[i].collider.gameObject.GetComponent<CharacterControl>().DecHP();
                        }

                    }
                }

            }
            if (!IsInCD)
            {
                StartCoroutine(WeaponCD());
            }
        }
    }
    IEnumerator WeaponCD()
    {
        IsInCD = true;
        yield return new WaitForSeconds(1f / HittedTimePerSec);
        IsInCD = false;
    }

    IEnumerator Aiming()
    {
        AimingLine = ObjectPool.GetInstance().GetObj(AimingLineName, "Rays");
        IsAiming = true;
        yield return new WaitForSeconds(AimingTime);
        ObjectPool.GetInstance().RecycleObj(AimingLine);
        IsAiming = false;
        StartCoroutine(Attacking());
    }

    IEnumerator Attacking()
    {
        Light = ObjectPool.GetInstance().GetObj(LightName, "Rays");
        IsAttacking = true;
        yield return new WaitForSeconds(AttackingTime);
        IsAttacking = false;
        ObjectPool.GetInstance().RecycleObj(Light);
    }
}
