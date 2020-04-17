using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public sealed class BattleUserConfig
{
    private static readonly BattleUserConfig instance = new BattleUserConfig();

    static BattleUserConfig()
    {
    }

    private BattleUserConfig()
    {
    }

    public static BattleUserConfig Instance
    {
        get
        {
            return instance;
        }
    }


    private bool IsAutoFire;

    public void SetAutoFire(bool bl) {
        IsAutoFire = bl;
    }
    public bool GetAutoFire() {
        return IsAutoFire;
    }

}
