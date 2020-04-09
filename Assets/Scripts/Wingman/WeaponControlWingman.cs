using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponControlWingman : WeaponControl
{
    private Vector3 shootPoint;
    private Wingman wingman;
    public string wingmanNum;

    private void Start()
    {
        WingmanJsonLoader loader = new WingmanJsonLoader();
        wingman = loader.LoadData(wingmanNum);
        LoadWeapon(wingman.WeaponNumber);
        if (weapon.isRay)
        {
            ray = ObjectPool.GetInstance().GetObj(weapon.RayNumber, "Bullets");
        }
    }

    public void Attack()
    {
        Shoot(transform.position - new Vector3(0, 0.4f, 0), Vector3.down);
    }

    public void ChangeWingmanNum(string curWingmanNum)
    {
        wingmanNum = curWingmanNum;
    }
}
