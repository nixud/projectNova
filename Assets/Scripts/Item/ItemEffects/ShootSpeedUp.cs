using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSpeedUp : ItemEffects
{
    public ShootSpeedUp(GameObject Obj)
    {
        time = 10;
        this.gameObject = Obj;
    }
    public override void Run() {
        gameObject.GetComponent<WeaponControl>().weapon.FireSpeed /= 2;
    }
    public override void End()
    {
        gameObject.GetComponent<WeaponControl>().weapon.FireSpeed *= 2;
    }
}
