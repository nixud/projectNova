using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillSelf : ItemEffects, IConsume
{
    public int EffectCount { get; }
    public float Cd { get; }

    public KillSelf()
    {
        EffectCount = 5;
        time = 0f;
        Cd = 0.5f;
    }


    public override void Run()
    {
        GameObject.Find("Player").GetComponent<CharacterControl>().DecHP();
    }

    public override void Update()
    {
        return;
    }

    public override void End()
    {
        return;
    }

    public override bool Condition()
    {
        return GameObject.Find("Player").transform.position.x > 0;
    }
}
