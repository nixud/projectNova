using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragElem : MonoBehaviour
{
    public Image image;
    [HideInInspector]public bool isDrag;

    [HideInInspector]public BagElemType elemType;
    [HideInInspector]public Item item;
    [HideInInspector]public WeaponNew weapon;
    [HideInInspector]public Wingman wingman;
    [HideInInspector]public GameObject deleteObject;
    public event Action onEndDragEvent;
    private void Start()
    {
        gameObject.SetActive(false);
        isDrag = false;
    }

    public void SetItem(GameObject elemObject, Sprite sprite, BagElemType type, Item item)
    {
        deleteObject = elemObject;
        image.sprite = sprite;
        elemType = type;
        this.item = item;
    }
    
    public void SetItem(GameObject elemObject, Sprite sprite, BagElemType type, WeaponNew weapon)
    {
        deleteObject = elemObject;
        image.sprite = sprite;
        elemType = type;
        this.weapon = weapon;
    }
    
    public void SetItem(GameObject elemObject, Sprite sprite, BagElemType type, Wingman wingman)
    {
        deleteObject = elemObject;
        image.sprite = sprite;
        elemType = type;
        this.wingman = wingman;
    }

    public void OnEndDrag()
    {
        onEndDragEvent?.Invoke();
    }
}
