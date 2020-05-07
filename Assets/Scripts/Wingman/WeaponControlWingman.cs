using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponControlWingman : MonoBehaviour
{
    private Vector3 shootPoint;
    private Wingman wingman;
    private string wingmanNum;
    public string WeaponNum;
    WeaponControl weaponControl;    //旧版实现
    WeaponNew weaponNew;

    private void Start()
    {
        WingmanJsonLoader loader = new WingmanJsonLoader();
        wingman = loader.LoadData(wingmanNum);
        weaponNew = WeaponLoader.LoadWeaponAndAttachToGO(WeaponNum, gameObject);
        /*
        weaponControl = gameObject.AddComponent<WeaponControl>();
        weaponControl.LoadWeapon(WeaponNum); // 旧weapon实现
        */
    }

    public void Init()
    {
        WingmanJsonLoader loader = new WingmanJsonLoader();
        wingman = loader.LoadData(wingmanNum);
        weaponNew = WeaponLoader.LoadWeaponAndAttachToGO(WeaponNum, gameObject);
    }

    public void Shoot(Vector3 shootPos,Vector3 shootForward)
    {
        weaponNew.Shoot(shootPos, shootForward);
        // weaponControl.Shoot(shootPos, shootForward);// 旧weapon实现
    }

    public void ChangeWingmanNum(string curWingmanNum)
    {
        wingmanNum = curWingmanNum;
    }


}
