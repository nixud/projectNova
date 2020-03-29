using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//控制敌人的武器的类，继承了weaponcontrol。
public class WeaponControlEnemy : WeaponControl
{
    private Vector3 shootPoint;
    public string WeaponName;

    /*
    private bool IsNotRecycled = false;
    private void OnEnable()
    {
        IsNotRecycled = true;
    }
    private void OnDisable()
    {
        IsNotRecycled = false;
    }*/

    private void Start()
    {
        LoadWeapon(WeaponName);
        if (weapon.isRay)
        {
            ray = ObjectPool.GetInstance().GetObj(weapon.RayNumber, "Bullets");
        }
    }

    public void ShootBehaviour() {
        Shoot(transform.position - new Vector3(0, 0.4f, 0), Vector3.down);
    }
}
