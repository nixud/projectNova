using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public class WeaponLoader : MonoBehaviour
{
    [Hotfix]
    public static WeaponNew LoadWeaponAndAttachToGO(string WeaponNumber,GameObject go) {
        WeaponJsonLoader jsonLoader = new WeaponJsonLoader();
        Weapon weapon = jsonLoader.LoadData(WeaponNumber);

        WeaponNew weaponNew;

        if (weapon.isRay == true)
        {
            weaponNew = go.AddComponent<WeaponRay>();
        }
        else {
            if (weapon.IsAShotgun == false)
            {
                weaponNew = go.AddComponent<WeaponNormalGun>();
            }
            else
            {
                weaponNew = go.AddComponent<WeaponShotGun>();
            }
        }

        weaponNew.LoadInfomation(weapon);

        return weaponNew;
    }
}
