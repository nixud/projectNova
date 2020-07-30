using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerSelect
{
    private PlayerSelect() { }
    private static PlayerSelect _instance;
    
    public static PlayerSelect Instance
    {
        get
        {
            if (_instance == null)
                _instance = new PlayerSelect();
            return _instance;
        }
    }

    public static readonly string Ship1Path = @"Prefabs/PlayerModel0";
    public static readonly string Ship2Path = @"Prefabs/PlayerModel1";

    public List<string> wingmansNumbers = new List<string>();
    
    public string PlayerShip = "Player0";

    public string PlayerShipPath
    {
        get => @"Prefabs/" + PlayerShip;
    }
}
