using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelShow : MonoBehaviour
{
    public Text Fuel;
    public Text Gold;

    private void Start()
    {
        Fuel.text = FuelInfo.Fuel.ToString();
        Gold.text = FuelInfo.Gold.ToString();
    }
}
