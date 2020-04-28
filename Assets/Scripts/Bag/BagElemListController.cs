using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagElemListController : MonoBehaviour
{
    public GameObject list;
    public ToggleGroup toggleGroup;
    
    private static readonly string _perfabPath = @"Prefabs/ItemAbout/ListElem";
    private GameObject _perfab;

    private BagUIController _bagUiController;
    private List<GameObject> _perfabs;
    private void Awake()
    {
        _perfab = Resources.Load<GameObject>(_perfabPath);
        _bagUiController = transform.GetComponentInParent<BagUIController>();
        _perfabs = new List<GameObject>();
    }

    private void OnDisable()
    {
        for (int i = _perfabs.Count - 1; i >= 0; i--)
        {
            Destroy(_perfabs[i]);
        }
        _perfabs.Clear();
    }

    public void RefreshList()
    {
        foreach (var weapon in PlayerStatus.GetInstance().Weapons)
        {
            var p = Instantiate(_perfab, list.transform.GetChild(0));
            p.transform.localScale = Vector3.one;
            p.GetComponent<BagListElem>().BagListElemInit(_bagUiController, toggleGroup,  weapon : weapon);
            
            _perfabs.Add(p);
        }

        foreach (var equipment in PlayerStatus.GetInstance().Equipments)
        {
            var p = Instantiate(_perfab, list.transform.GetChild(0));
            p.transform.localScale = Vector3.one;
            p.GetComponent<BagListElem>().BagListElemInit(_bagUiController, toggleGroup, item : equipment);
            
            _perfabs.Add(p);
        }

        foreach (var wingman in PlayerStatus.GetInstance().Wingmans)
        {
            var p = Instantiate(_perfab, list.transform.GetChild(0));
            p.transform.localScale = Vector3.one;
            p.GetComponent<BagListElem>().BagListElemInit(_bagUiController, toggleGroup, wingman : wingman);
            
            _perfabs.Add(p);
        }
    }
}
