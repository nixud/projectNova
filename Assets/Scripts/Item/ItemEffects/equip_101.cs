using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 
public class equip_101 : ItemEffects
{
    private GameObject player;
    private WeaponNormalGun[] wng;
    private WeaponShotGun[] wsg;
    private readonly float rate = 2f;
    private float realRate
    {
        get => 1 / rate;
    } 

    public equip_101()
    {
        time = 10f;
    }
    public override void Run()
    {
        player = GameObject.Find("Player");

        wng = player.GetComponents<WeaponNormalGun>();
        wsg = player.GetComponents<WeaponShotGun>();
        
        // 获取武器组件貌似行为不对劲
        // foreach (var weapon in characterControl.weaponNews)
        // {
        //     ((WeaponNormalGun) weapon).FireSpeed *= 2;
        // }
        foreach (var gun in wng)
        {
            gun.FireSpeed *= realRate;
        }

        foreach (var gun in wsg)
        {
            gun.FireSpeed *= realRate;
        }
    }

    public override void Update()
    { return; }

    public override void End()
    {
        foreach (var gun in wng)
        {
            gun.FireSpeed /= realRate;
        }

        foreach (var gun in wsg)
        {
            gun.FireSpeed /= realRate;
        }
    }

    public override bool Condition()
    {
        return true;
    }
    
}
