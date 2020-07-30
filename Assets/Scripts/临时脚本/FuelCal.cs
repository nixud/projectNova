using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelCal : MonoBehaviour
{
    public Text fuel;
    public Text gold;

    public Text text;

    private void Start()
    {
        text.text = "扫描仪待命中";
        gold.text = "0";
    }

    private void Update()
    {
        fuel.text = FuelInfo.Fuel.ToString();
    }
}
