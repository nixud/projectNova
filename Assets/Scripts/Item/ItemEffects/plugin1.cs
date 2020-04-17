using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plugin1 : ItemEffects
{
    private UIcontroller _uIcontroller;

    public plugin1()
    {
        _uIcontroller = GameObject.Find("Canvas").GetComponent<UIcontroller>();
    }
    public override void Run()
    {
        PlayerStatus.GetInstance().HP += 2f;
        _uIcontroller++;
        _uIcontroller++;
    }

    public override void Update()
    {
        // Debug.Log("plugin zhengchang");
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
