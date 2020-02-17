using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus
{
    #region 单例
    private static PlayerStatus instance;
    private PlayerStatus()
    {
    }
    public static PlayerStatus GetInstance()
    {
        if (instance == null)
        {
            instance = new PlayerStatus();
        }
        return instance;
    }

    #endregion

    public string CommanderName;
    public string CommanderGroupName;
    public int CommanderAvatarNumber;
    public struct CommanderGroupLogo
    {
        int FrontPic;
        int BackPic;
    };

    private int CommanderPicNumber;
    private int CommanderGroupPicNumber;

    public int StarCoins = 233;

    public int Fuel = 114;

    public float HP = 3;

    private Ship ship;
    private Plugin plugin;

    private List<Weapon> weapons;
    private List<Plugin> plugins;
    private List<Item> items;
}
