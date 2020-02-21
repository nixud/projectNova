using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSpeedUp : ItemEffects
{
    public ShootSpeedUp()
    {
        time = 4;
    }
    public override void Run() {
        GameObject.Find("Player").GetComponent<CharacterControl>().WeaponSpeedChange("/",3);
    }
    public override void End()
    {
        GameObject.Find("Player").GetComponent<CharacterControl>().WeaponSpeedChange("*", 3);
    }
}
