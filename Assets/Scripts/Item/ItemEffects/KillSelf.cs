using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillSelf : ItemEffects
{
    public override void Run()
    {
        GameObject.Find("Player").GetComponent<CharacterControl>().DecHP();
    }

    public override void End()
    {
        return;
    }
}
