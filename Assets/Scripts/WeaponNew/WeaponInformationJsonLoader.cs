using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class WeaponInformationJsonLoader
{
    
    public List<WeaponInformation> LoadData() {
        JsonLoader<WeaponInformation> loader = new JsonLoader<WeaponInformation>();
        return loader.LoadData();
    }

    public WeaponInformation LoadData(string WeaponNum)
    {
        JsonLoader<WeaponInformation> loader = new JsonLoader<WeaponInformation>();
        List<WeaponInformation> weaponlist = new List<WeaponInformation>();

        weaponlist = loader.LoadData();

        WeaponInformation returnWeapon = new WeaponInformation();
        for (int i=0;i<weaponlist.Count;i++) {
            if (weaponlist[i].Number == WeaponNum)
                returnWeapon = weaponlist[i];
        }

        return returnWeapon;
    }

    public void SaveData(WeaponInformation weapon) {
        JsonLoader<WeaponInformation> loader = new JsonLoader<WeaponInformation>();
        List<WeaponInformation> weaponlist = new List<WeaponInformation>();

        weaponlist = LoadData();

        int index=0,i;
        for (i=0;i<weaponlist.Count;i++) {
            if (weaponlist[i].Number == weapon.Number)
                break;
        }
        index = i;
        if (index != weaponlist.Count)
        {
            weaponlist.RemoveAt(index);
        }
        weaponlist.Insert(index,weapon);

        loader.SaveData(weaponlist);
    }
    public void DeleteData(int index, List<WeaponInformation> weaponlist) {

        weaponlist.RemoveAt(index);
        JsonLoader<WeaponInformation> loader = new JsonLoader<WeaponInformation>();

        //weaponlist = WeaponSort(weaponlist);

        loader.SaveData(weaponlist);
    }
    public List<WeaponInformation> WeaponSort(List<WeaponInformation> weapons) {
        if(weapons.Count>=1)
            weapons.Sort();
        return weapons;
    } 
}
