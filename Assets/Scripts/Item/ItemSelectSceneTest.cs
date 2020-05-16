using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelectSceneTest : MonoBehaviour
{
    public int ItemNum;

    private Item _item;
    public Color pressColor, normalColor;

    private void Start()
    {
        GetComponent<Image>().color = normalColor;
    }

    private Item GetItem()
    {
        if (_item != null)
            return _item;
        _item = ItemLoader.LoadData(ItemNum);
        return _item;
    }
    
    public void OnClick()
    {
        if (GetComponent<Toggle>().isOn)
        {
            Debug.Log("add");
            GetComponent<Image>().color = pressColor;
            if (ItemNum - 200 > 0)
            {
                PlayerStatus.GetInstance().Plugins.Add(GetItem());
            }
            else
            {
                PlayerStatus.GetInstance().Equipments.Add(GetItem());
            }
        }
        else
        {
            Debug.Log("remove");
            GetComponent<Image>().color = normalColor;
            if (ItemNum - 200 > 0)
            {
                PlayerStatus.GetInstance().Plugins.Remove(GetItem());
            }
            else
            {
                PlayerStatus.GetInstance().Equipments.Remove(GetItem());
            }
        }
    }
}
