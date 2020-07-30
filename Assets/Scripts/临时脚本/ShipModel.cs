using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 临时添加脚本实现飞船选择是射击效果
public class ShipModel : MonoBehaviour
{
    
    List<WeaponControl> weaponControl = new List<WeaponControl>();
    public List<WeaponNew> weaponNews = new List<WeaponNew>();
    public List<GameObject> shootPoints = new List<GameObject>();
 
    public List<string> WeaponName = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        gameObject.name = "Player";
        for (int i = 0; i < WeaponName.Count; i++)
        {
            weaponNews.Add(WeaponLoader.LoadWeaponAndAttachToGO(WeaponName[i],gameObject));
        }
        
        if (BattleUserConfig.Instance.GetAutoFire() == true)
        {
            StartRay();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
            Shoot();
    }
    
    public void Shoot()
    {
        //for (int i = 0; i < weaponControl.Count; i++)
        //    weaponControl[i].Shoot(shootPoints[i].transform.position, Vector3.up);
        for (int i = 0; i < weaponNews.Count; i++) {
            weaponNews[i].Shoot(shootPoints[i].transform.position, Vector3.up);
        }
    }
    
    public void StartRay()
    {
        for (int i = 0; i < weaponControl.Count; i++)
            weaponControl[i].StartRay();
    }
}
