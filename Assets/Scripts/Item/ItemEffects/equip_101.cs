using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 
public class equip_101 : ItemEffects
{
    private GameObject player;
    private Component[] w;
    private readonly float rate = 2f;
    
    public equip_101()
    {
        time = 10f;
    }
    public override void Run()
    {
        player = GameObject.Find("Player");
        
        // 获取武器组件貌似行为不对劲
        // foreach (var weapon in characterControl.weaponNews)
        // {
        //     ((WeaponNormalGun) weapon).FireSpeed *= 2;
        // }
        w = player.GetComponents(typeof(WeaponNormalGun));
        foreach (var component in w)
        {
            ((WeaponNormalGun) component).FireSpeed *= 1 / rate;
        }
    }

    public override void Update()
    { return; }

    public override void End()
    {
        foreach (var component in w)
        {
            ((WeaponNormalGun) component).FireSpeed /= 1 / rate;
        }
    }

    public override bool Condition()
    {
        return true;
    }
}
