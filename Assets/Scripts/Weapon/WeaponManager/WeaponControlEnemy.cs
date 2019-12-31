using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponControlEnemy : WeaponControl
{
    private Vector3 shootPoint;
    public string WeaponName;
    private void Start()
    {
        LoadWeapon(WeaponName);
        if (weapon.isRay)
        {
            ray = ObjectPool.GetInstance().GetObj(weapon.RayNumber, "Bullets");
        }
    }
    private void Update()
    {
        Shoot(transform.position - new Vector3(0,0.4f,0),Vector3.down);
    }

}
