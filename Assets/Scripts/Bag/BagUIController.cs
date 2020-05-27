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

    [HideInInspector]public GameObject _gobjTemp;
    private Item _itemTemp;
    private GameObject _player;
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
        DeleteButton.GetComponent<Button>().interactable = true;
    }

    public void ShowElem(WeaponNew weapon, GameObject gobj)
    {
        elemName.text = weapon.WeaponName;
        elemDescription.text = weapon.Description;
        elemType.text = "类型：武器";
        elemLevel.text = "等级：" + weapon.rareLevel.ToString();

        DeleteButton.GetComponent<Button>().interactable = true;
    }

    public void ShowElem(Wingman wingman, GameObject gobj)
    {
        elemName.text = wingman.Name;
        elemDescription.text = wingman.Description;
        
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

    public void DeletePlugin()
    {    
        GameObject.Find("ItemControl").GetComponent<ItemControl>().DeletePlugin(_itemTemp);
        Destroy(_gobjTemp);
        
        RefreshInfo();
    }

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
}

public enum BagElemType
{
    Weapon,
    Wingman,
    Item
}
