using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.UIElements.GraphView;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BagUIController : MonoBehaviour
{
    public Text elemName;
    public Text elemEffect;
    public Text elemDescription;

    public GameObject DeleteButton;
    public GameObject SaleButton;

    public GameObject elemList;
    public GameObject pluginList;

    public GameObject _pluginTemp;
    private Item _itemTemp;
    
    private void OnEnable()
    {
        RefreshList();
        RefreshInfo();
    }

    void Start()
    {
        this.gameObject.SetActive(false);
    }

    public void ChangeBagActive()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void ShowElem(Item item, GameObject gobj)
    {
        elemName.text = item.Name;
        // ElemEffect.text = item.Effect;
        elemDescription.text = item.Description;

        _itemTemp = item;
        _pluginTemp = gobj;
        
        DeleteButton.SetActive(true);
        SaleButton.SetActive(true);
    }

    public void ShowElem(WeaponNew weapon)
    {
        elemName.text = weapon.WeaponName;
        // ElemEffect.text = weapon.Effect;
        elemDescription.text = weapon.Description;
    }

    public void ShowElem(Wingman wingman)
    {
        elemName.text = wingman.Name;
        // ElemEffect.text = wingman.Effect;
        elemDescription.text = wingman.Description;
    }

    public void DeletePlugin()
    {    
        GameObject.Find("ItemControl").GetComponent<ItemControl>().DeletePlugin(_itemTemp);
        Destroy(_pluginTemp);
        Debug.Log(_pluginTemp.GetComponent<BagListElem>()._item.Name);
        
        DeleteButton.SetActive(false);
        SaleButton.SetActive(false);
        
        RefreshInfo();
    }

    public void SalePlugin()
    {
        // return;
    }

    private void RefreshList()
    {
        // elemList.GetComponent<BagElemListController>().RefreshList();
        pluginList.GetComponent<BagPluginListController>().RefreshList();
    }

    private void RefreshInfo()
    {
        try
        {
            // 默认显示第一列表第一项
            //                 List         panel      first item
            // pluginList.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<BagListElem>().SetElemToShow();
            _pluginTemp = pluginList.GetComponent<BagPluginListController>().SetDefaultElem();
            _itemTemp = _pluginTemp.GetComponent<BagListElem>()._item;
        }
        catch (Exception e)
        {
            // ignored
        }
    }
}

public enum BagElemType
{
    Weapon,
    Wingman,
    Item
}
