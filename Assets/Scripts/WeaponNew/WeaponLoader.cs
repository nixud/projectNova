using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using System;

public class WeaponLoader : MonoBehaviour
{
    [Hotfix]
    public static WeaponNew LoadWeaponAndAttachToGO(string WeaponNumber,GameObject go) {
        WeaponInformationJsonLoader jsonLoader = new WeaponInformationJsonLoader();
        WeaponInformation weapon = jsonLoader.LoadData(WeaponNumber);

        WeaponNew weaponNew;

        if (weapon.WeaponSort == 1)
        {
            weaponNew = go.AddComponent<WeaponNormalGun>();
        }
        else if (weapon.WeaponSort == 2)
        {
            weaponNew = go.AddComponent<WeaponShotGun>();
        }
        else if (weapon.WeaponSort == 3)
        {
            weaponNew = go.AddComponent<WeaponRay>();
        }
        else if(weapon.WeaponSort == 4)
            weaponNew = go.AddComponent<WeaponMechineGun>();
        else throw new Exception();

        weaponNew.LoadInfomation(weapon);

        return weaponNew;
    }
}
