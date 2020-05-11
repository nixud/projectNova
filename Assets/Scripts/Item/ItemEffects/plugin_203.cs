using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plugin_203 : ItemEffects
{
    private readonly float changeRate = 1.2f;
    private GameObject player;
    public plugin_203()
    {    
        player = GameObject.Find("Player");
    }
    public override void Run()
    {
        player.GetComponent<CharacterControl>().WeaponSpeedChange("/", changeRate);
        Debug.Log("plugin_203 start");
    }

    public override void Update()
    {
    }

    public override void End()
    {
        player.GetComponent<CharacterControl>().WeaponSpeedChange("*", changeRate);
    }

    public override bool Condition()
    {
        return true;
    }
}
