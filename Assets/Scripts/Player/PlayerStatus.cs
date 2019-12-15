using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    private static PlayerStatus instance = null;
    private static readonly object padlock = new object();

    PlayerStatus()
    {
    }

    public static PlayerStatus Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new PlayerStatus();
                }
                return instance;
            }
        }
    }

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

    public int StarCoins;

    public int Fuel;

    private Ship ship;
    private Plugin plugin;

    private List<Weapon> weapons;
    private List<Plugin> plugins;
    private List<Item> items;
}
