using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 拾捡道具按钮，周围无道具自动disable
public class PickButton : MonoBehaviour
{
    public Image image;

    private GameObject _equipObject;
    private Item _equipment;
    private ItemControl _itemControl;

    private void Update()
    {
        if (_equipment == null)
            gameObject.SetActive(false);
    }

    // onClick
    public void Pick()
    {
        if (_itemControl == null)
            _itemControl = transform.GetComponentInParent<ItemControl>();
        _itemControl.GetEquipment(_equipment);

        Destroy(_equipObject);
        this._equipment = null;
    }
    
    /// <summary>
    /// 装备拾取道具
    /// </summary>
    /// <param name="equip"></param>
    /// <param name="gobj"></param>
    public void SetEquipToPick(Item equip, GameObject gobj)
    {
        _equipment = equip;
        _equipObject = gobj;
        if (_equipment == null)
            return;
        image.sprite = Resources.Load<Sprite>(_equipment.PicPath);
    }
}
