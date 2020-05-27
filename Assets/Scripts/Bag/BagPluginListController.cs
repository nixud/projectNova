using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BagPluginListController : MonoBehaviour
{
    public GameObject list;
    public ToggleGroup toggleGroup;
    public BagUIController bagUiController;

    private static readonly string _perfabPath = @"Prefabs/ItemAbout/ListElem";
    private GameObject _perfab;

    private List<GameObject> _perfabs = new List<GameObject>();

    private void Start()
    {
    }

    private void OnDisable()
    {
        try
        {
            for (int i = _perfabs.Count - 1; i >= 0; i--)
            {
                Destroy(_perfabs[i]);
            }
            
            _perfabs.Clear();
        }
        catch (Exception e)
        {
            // ignored
        }
    }

    public void RefreshList()
    {
        if (_perfab == null)
        {
            _perfab = Resources.Load<GameObject>(_perfabPath);
        }
        
        foreach (var plugin in PlayerStatus.GetInstance().Plugins)
        {
            var p = Instantiate(_perfab, list.transform);
            p.transform.localScale = Vector3.one;
            p.GetComponent<BagListElem>().BagListElemInit(bagUiController, toggleGroup, plugin);
    
            _perfabs.Add(p);
        }
    }

    // public GameObject SetDefaultElem()
    // {
        // var p = list.transform.GetChild(0).GetComponent<BagListElem>();
        // Debug.Log("default: " + p._item.Name);
        // Debug.Log(p.transform.position);
        // p.SetElemToShowDefault();
        // return p.gameObject;
    // }
}