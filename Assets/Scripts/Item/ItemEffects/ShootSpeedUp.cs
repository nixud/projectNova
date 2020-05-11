using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSpeedUp : ItemEffects
{
    public ShootSpeedUp()
    {
        time = 10f;
    }
    public override void Run() {
        GameObject.Find("Player").GetComponent<CharacterControl>().WeaponSpeedChange("/",2);
        
        Debug.Log("equip_101 start");
    }

    public override void Update()
    { return; }

    public override void End()
    {
        GameObject.Find("Player").GetComponent<CharacterControl>().WeaponSpeedChange("*", 2);
        
        Debug.Log("equip_101 end");
    }

    public override bool Condition()
    {
        return true;
    }
}
