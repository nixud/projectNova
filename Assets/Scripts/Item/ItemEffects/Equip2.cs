using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equip2 : ItemEffects, IAccumulate
{
    public int Accumulate { get; }
    public Equip2()
    {
        Accumulate = 1000;
        time = 4f;
    }
    public override void Run() {
        GameObject.Find("Player").GetComponent<CharacterControl>().WeaponSpeedChange("/",3);
    }

    public override void Update()
    { return; }

    public override void End()
    {
        GameObject.Find("Player").GetComponent<CharacterControl>().WeaponSpeedChange("*", 3);
    }

    public override bool Condition()
    {
        return true;
    }

}
