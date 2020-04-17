using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equip1 : ItemEffects, IConsume
{
    public int EffectCount { get; }
    public float Cd { get; }

    public Equip1()
    {
        EffectCount = 5;
        time = 0f;
        Cd = 0.5f;
    }


    public override void Run()
    {
        Debug.Log("Test1 run");
    }

    public override void Update()
    {
        Debug.Log("Test1 update");
    }

    public override void End()
    {
        Debug.Log("Test1 end");
    }

    public override bool Condition()
    {
        return true;
    }
}
