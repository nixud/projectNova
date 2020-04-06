using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLoader : MonoBehaviour
{
    static WeaponNew LoadWeapon(string WeaponNumber) {
        WeaponJsonLoader jsonLoader = new WeaponJsonLoader();
        Weapon weapon = jsonLoader.LoadData(WeaponNumber);

        WeaponNew result = new WeaponNormalGun(weapon);

        if (weapon.isRay == false) {
            if (weapon.IsAShotgun == false) {
                result = new WeaponNormalGun(weapon);
            }
        }

        return result;
    }
}
