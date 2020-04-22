using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plugin_201 : ItemEffects
{
    private UIcontroller _uIcontroller;

    public plugin_201()
    {
        _uIcontroller = GameObject.Find("Canvas").GetComponent<UIcontroller>();
    }
    public override void Run()
    {
        PlayerStatus.GetInstance().HP += 4f;
        _uIcontroller++;
        _uIcontroller++;
        _uIcontroller++;
        _uIcontroller++;
    }

    public override void Update()
    {
        return;
    }

    public override void End()
    {
        PlayerStatus.GetInstance().HP -= 4f;
        _uIcontroller--;
        _uIcontroller--;
        _uIcontroller--;
        _uIcontroller--;
    }

    public override bool Condition()
    {
        return true;
    }
}
