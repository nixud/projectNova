﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BagPluginListController : MonoBehaviour
{
    public GameObject list;
    public ToggleGroup toggleGroup;

    private static readonly string _perfabPath = @"Prefabs/ItemAbout/ListElem";
    private GameObject _perfab;

    private BagUIController _bagUiController;
    private List<GameObject> _perfabs;

    private void Start()
    {
        _perfab = Resources.Load<GameObject>(_perfabPath);
        _bagUiController = transform.GetComponentInParent<BagUIController>();
        _perfabs = new List<GameObject>();
    }

    private void OnDisable()
    {
        try
        {
            for (int i = _perfabs.Count - 1; i >= 0; i--)
            {
                Destroy(_perfabs[i]);
            }
        }
        catch (Exception e)
        {
            // ignored
        }

        _perfabs.Clear();
    }

    public void RefreshList()
    {
        foreach (var plugin in PlayerStatus.GetInstance().Plugins)
        {
            var p = Instantiate(_perfab, list.transform);
            p.transform.localScale = Vector3.one;
            p.GetComponent<BagListElem>().BagListElemInit(_bagUiController, toggleGroup, plugin);
    
            _perfabs.Add(p);
        }            
    }

    public void SetDefaultElem()
    {
        transform.GetComponentInChildren<BagListElem>().SetElemToShow();
    }
}            