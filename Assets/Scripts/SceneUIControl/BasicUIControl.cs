using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicUIControl : MonoBehaviour
{
    public GameObject FulNum;
    public GameObject MoneyNum;
    public void BasicInit()
    {
        FulNum = GameObject.Find("FulNum");
        MoneyNum = GameObject.Find("MoneyNum");
        FulNum.GetComponent<Text>().text = PlayerStatus.Instance.Fuel.ToString();
        MoneyNum.GetComponent<Text>().text = PlayerStatus.Instance.StarCoins.ToString();
    }
    public void FreshData() {
        FulNum.GetComponent<Text>().text = PlayerStatus.Instance.Fuel.ToString();
        MoneyNum.GetComponent<Text>().text = PlayerStatus.Instance.StarCoins.ToString();
    }
}
