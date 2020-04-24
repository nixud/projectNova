using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BagListElem : MonoBehaviour
{
    public Image elemIcon;
    public Toggle toggle;
    public Image pressedEdge;
    
    private BagUIController _bagUiController;
    private BagElemType _type;

    private Item _item;
    private WeaponNew _weapon;
    private Wingman _wingman;

    public void BagListElemInit(BagUIController bagUiController,ToggleGroup toggleGroup, Item item = null, WeaponNew weapon = null, Wingman wingman = null)
    {
        _bagUiController = bagUiController;
        if (item != null)
        {
            _type = BagElemType.Item;
            _item = item;
            elemIcon.sprite = Resources.Load<Sprite>(item.PicPath);
        }
        else if (weapon != null)
        {
            _type = BagElemType.Weapon;
            _weapon = weapon;
            elemIcon.sprite = Resources.Load<Sprite>(weapon.IconPath);
        }
        else if (wingman != null)
        {
            _type = BagElemType.Wingman;
            _wingman = wingman;
            elemIcon.sprite = Resources.Load<Sprite>(wingman.iconPath);
        }
        else
            throw  new Exception("Can't identify class");

        toggle.group = toggleGroup;
        pressedEdge.gameObject.SetActive(false);
    }

    public void SetElemToShow()
    {
        // if (!toggle.isOn)
        //     return;
        if (_type == BagElemType.Item)
            _bagUiController.ShowElem(_item, this.gameObject);
        else if (_type == BagElemType.Weapon)
            _bagUiController.ShowElem(_weapon);
        else if (_type == BagElemType.Wingman)
            _bagUiController.ShowElem(_wingman);
        
        Debug.Log(_item.Name);
    }

    public void SetEdge()
    {
        if (toggle.isOn)
            pressedEdge.gameObject.SetActive(true);
        else 
            pressedEdge.gameObject.SetActive(false);
    }
}
