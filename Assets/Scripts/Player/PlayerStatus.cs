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

    public float HP = 9;
    public float MaxHP = 9;

    private Ship ship;
    private Plugin plugin;

    // private List<Weapon> weapons;
    // private List<Plugin> plugins;
    // private List<Item> items;
    public List<WeaponNew> Weapons = new List<WeaponNew>();
    public List<Item> Plugins = new List<Item>();
    public List<Item> Equipments = new List<Item>();
    public List<Wingman> Wingmans = new List<Wingman>();
}
