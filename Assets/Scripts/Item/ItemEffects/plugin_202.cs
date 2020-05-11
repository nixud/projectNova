using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plugin_202 : ItemEffects
{
    private UIcontroller _uIcontroller;

    public plugin_202()
    {

    }
    public override void Run()
    {
        Debug.Log("plugin_202 start");
    }

    public override void Update()
    {
        return;
    }

    public override void End()
    {

    }

    public override bool Condition()
    {
        return true;
    }
}
