using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class WeaponJsonLoader
{
    
    public List<Weapon> LoadData() {
        JsonLoader<Weapon> loader = new JsonLoader<Weapon>();
        return loader.LoadData();
    }

    public Weapon LoadData(string WeaponNum)
    {
        JsonLoader<Weapon> loader = new JsonLoader<Weapon>();
        List<Weapon> weaponlist = new List<Weapon>();

        weaponlist = loader.LoadData();

        Weapon returnWeapon = new Weapon();
        for (int i=0;i<weaponlist.Count;i++) {
            if (weaponlist[i].Number == WeaponNum)
                returnWeapon = weaponlist[i];
        }

        return returnWeapon;
    }

    public void SaveData(Weapon weapon) {
        JsonLoader<Weapon> loader = new JsonLoader<Weapon>();
        List<Weapon> weaponlist = new List<Weapon>();

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
    public void DeleteData(int index, List<Weapon> weaponlist) {

        weaponlist.RemoveAt(index);
        JsonLoader<Weapon> loader = new JsonLoader<Weapon>();

        //weaponlist = WeaponSort(weaponlist);

        loader.SaveData(weaponlist);
    }
    public List<Weapon> WeaponSort(List<Weapon> weapons) {
        if(weapons.Count>=1)
            weapons.Sort();
        return weapons;
    } 
}
