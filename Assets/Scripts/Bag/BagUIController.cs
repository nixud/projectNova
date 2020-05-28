using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.UIElements.GraphView;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BagUIController : MonoBehaviour
{
    public Text elemName;
    public Text elemDescription;
    public Text elemType;
    public Text elemLevel;

    public GameObject DeleteButton;

    public GameObject elemList;
    public GameObject pluginList;

    private GameObject _gobjTemp;
    private BagElemType _deleteTyoe;
    private Item _itemTemp;
    private WeaponNew _weaponTemp;
    private Wingman _wingmanTemp;
    private GameObject _player;

    private static readonly string dragItemPath = @"Prefabs/ItemAbout/DragItem"; 
    private void OnEnable()
    {
        RefreshList();
        RefreshInfo();
    }

    void Start()
    {
        // this.gameObject.SetActive(false);
    }

    public void ChangeBagActive()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void ShowElem(Item item, GameObject gobj)
    {
        elemName.text = item.Name;
        elemDescription.text = item.Description;
        elemType.text = "类型：" + (item.Type == ItemType.Plugin ? "插件" : "道具");
        elemLevel.text = "等级：" + item.rareLevel.ToString();
        
        _itemTemp = item;
        _gobjTemp = gobj;
        _deleteTyoe = BagElemType.Item;
        DeleteButton.GetComponent<Button>().interactable = true;
    }

    public void ShowElem(WeaponNew weapon, GameObject gobj)
    {
        elemName.text = weapon.WeaponName;
        elemDescription.text = weapon.Description;
        elemType.text = "类型：武器";
        elemLevel.text = "等级：" + weapon.rareLevel.ToString();

        _weaponTemp = weapon;
        _gobjTemp = gobj;
        _deleteTyoe = BagElemType.Weapon;
        DeleteButton.GetComponent<Button>().interactable = true;
    }

    public void ShowElem(Wingman wingman, GameObject gobj)
    {
        elemName.text = wingman.Name;
        elemDescription.text = wingman.Description;
        elemType.text = "类型：僚机";
        elemLevel.text = "等级：" + wingman.rareLevel.ToString();

        _wingmanTemp = wingman;
        _gobjTemp = gobj;
        _deleteTyoe = BagElemType.Wingman;
        DeleteButton.GetComponent<Button>().interactable = true;
    }

    public void ShowElem(Ship ship)
    {
        // 目前ship为空的解决方案
        if (ship == null)
        {
            elemName.text = "这个作者很懒什么都没有留下";
            elemDescription.text = "这个作者很懒什么都没有留下";
            elemType.text = "类型：旗舰";
        }
        else
        {
            elemName.text = ship.Name;
            elemDescription.text = ship.Description;
            elemType.text = "类型：旗舰";
        }

        DeleteButton.GetComponent<Button>().interactable = false;
    }

    public void Delete()
    {    
        if (_deleteTyoe == BagElemType.Item)
            DeleteItem(_gobjTemp, _itemTemp);
        else if (_deleteTyoe == BagElemType.Weapon)
            DeleteWeapon(_gobjTemp, _weaponTemp);
        else if (_deleteTyoe == BagElemType.Wingman)
            DeleteWingman(_gobjTemp, _wingmanTemp);
        else 
            throw new Exception("Delete type not match");
        
        RefreshInfo();
    }

    public void Delete(GameObject dragItem)
    {
        var dragElem = dragItem.GetComponent<DragElem>();
        if (dragElem.elemType == BagElemType.Item)
            DeleteItem(dragElem.deleteObject, dragElem.item);
        else if (dragElem.elemType == BagElemType.Weapon)
            DeleteWeapon(dragElem.deleteObject, dragElem.weapon);
        else if (dragElem.elemType == BagElemType.Wingman)
            DeleteWingman(dragElem.deleteObject, dragElem.wingman);
        else 
            throw new Exception("Delete type not match");
    }

    #region DeleteHelper

    private void DeleteWeapon(GameObject gobj, WeaponNew weapon)
    {
        
    }

    private void DeleteWingman(GameObject gobj, Wingman wingman)
    {
        
    }

    private void DeleteItem(GameObject gobj, Item item)
    {
        if (item.Type == ItemType.Plugin)
        {
            GameObject.Find("ItemControl").GetComponent<ItemControl>().DeletePlugin(item);
            Destroy(gobj);
        }
        else
        {
            
        }
    }

    #endregion // DeleteHelper
    
    public void CloseBagList()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    private void RefreshList()
    {
        elemList.GetComponent<BagElemListController>().RefreshList();
        pluginList.GetComponent<BagPluginListController>().RefreshList();
    }

    private void RefreshInfo()
    {
        if (_player == null)
            _player = GameObject.Find("Player");
        ShowElem(_player.GetComponent<CharacterControl>().ship);
    }

    private GameObject _dragHelper;
    public GameObject DragHelper
    {
        get
        {
            if (_dragHelper == null)
            {
                _dragHelper = Resources.Load<GameObject>(dragItemPath);
                _dragHelper = Instantiate(_dragHelper, transform);
                _dragHelper.transform.position = new Vector3(1000, 1000);
            }
    
            return _dragHelper;
        }
    }
}

public enum BagElemType
{
    Weapon,
    Wingman,
    Item
}
