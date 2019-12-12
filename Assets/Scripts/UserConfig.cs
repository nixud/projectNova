using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public sealed class UserConfig
{
    private static readonly UserConfig instance = new UserConfig();

    static UserConfig()
    {
    }

    private UserConfig()
    {
    }

    public static UserConfig Instance
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
