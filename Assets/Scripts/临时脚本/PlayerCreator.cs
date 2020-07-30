using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCreator : MonoBehaviour
{
    private static readonly string PlayerName = "Player";
    
    private void Start()
    {
        var player = Resources.Load<GameObject>(PlayerSelect.Instance.PlayerShipPath); 
        var pobj = Instantiate(player);
        pobj.name = PlayerName;
    }
}
