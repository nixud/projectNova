using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLoader : MonoBehaviour
{
    public static WeaponNew LoadWeaponAndAttachToGO(string WeaponNumber,GameObject go) {
        WeaponJsonLoader jsonLoader = new WeaponJsonLoader();
        Weapon weapon = jsonLoader.LoadData(WeaponNumber);

        WeaponNew weaponNew;

        weaponNew = go.AddComponent<WeaponNormalGun>();

        weaponNew.LoadInfomation(weapon);

        return weaponNew;
    }
}
