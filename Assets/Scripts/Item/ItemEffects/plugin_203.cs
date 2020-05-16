using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 装填强化
public class plugin_203 : ItemEffects
{
    private readonly float rate = 1.2f;
    private WeaponNormalGun[] wng;
    private WeaponShotGun[] wsg;
    private GameObject player;

    private float realRate
    {
        get => 1 / rate;
    }

    public plugin_203()
    {    
        
    }
    public override void Run()
    {
        player = GameObject.Find("Player");

        wng = player.GetComponents<WeaponNormalGun>();
        wsg = player.GetComponents<WeaponShotGun>();
        foreach (var gun in wng)
        {
            gun.FireSpeed *= realRate;
        }

        foreach (var gun in wsg)
        {
            gun.FireSpeed *= realRate;
        }
        Debug.Log("203 run");
    }

    public override void Update()
    {
    }

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
