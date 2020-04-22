using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plugin3 : ItemEffects
{
    private BulletNew[] bullets;
    private float damageRate;
    public plugin3()
    {
        damageRate = 1.3f;
    }
    public override void Run()
    {

    }

    public override void Update()
    {
        Debug.Log("p3 update");
    }

    public override void End()
    {
        return;
    }

    public override bool Condition()
    {
        return true;
    }
}
