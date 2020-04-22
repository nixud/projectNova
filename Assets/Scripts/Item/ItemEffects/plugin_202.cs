using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plugin_202 : ItemEffects
{
    private UIcontroller _uIcontroller;

    public plugin_202()
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
        return;
    }

    public override void End()
    {
        PlayerStatus.GetInstance().HP -= 2f;
        _uIcontroller--;
        _uIcontroller--;
    }

    public override bool Condition()
    {
        return true;
    }
}
