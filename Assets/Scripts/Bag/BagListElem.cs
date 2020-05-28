using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BagListElem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Image elemIcon;
    public Toggle toggle;
    public Image pressedEdge;

    private BagUIController _bagUiController;
    private BagElemType _type;

    private Item _item;
    private WeaponNew _weapon;
    private Wingman _wingman;

    private Sprite _sprite;
    private GameObject _dragItem;
    private void Start()
    {
        _dragItem = GameObject.Find("EquipCanvas").GetComponent<BagUIController>().DragHelper;
    }

    public void BagListElemInit(BagUIController bagUiController,ToggleGroup toggleGroup, Item item = null, WeaponNew weapon = null, Wingman wingman = null)
    {
        _bagUiController = bagUiController;
        if (item != null)
        {
            _type = BagElemType.Item;
            this._item = item;
            _sprite = Resources.Load<Sprite>(item.PicPath);
            elemIcon.sprite = _sprite;
        }
        else if (weapon != null)
        {
            _type = BagElemType.Weapon;
            _weapon = weapon;
            _sprite = Resources.Load<Sprite>(weapon.IconPath);
            elemIcon.sprite = _sprite;
        }
        else if (wingman != null)
        {
            _type = BagElemType.Wingman;
            _wingman = wingman;
            _sprite = Resources.Load<Sprite>(wingman.iconPath);
            elemIcon.sprite = _sprite;
        }
        else
            throw  new Exception("Can't identify class");

        toggle.group = toggleGroup;
        pressedEdge.gameObject.SetActive(false);
    }

    public void SetElemToShow()
    {
        if (!toggle.isOn)
            return;
        if (_type == BagElemType.Item)
            _bagUiController.ShowElem(_item, this.gameObject);
        else if (_type == BagElemType.Weapon)
            _bagUiController.ShowElem(_weapon, this.gameObject);
        else if (_type == BagElemType.Wingman)
            _bagUiController.ShowElem(_wingman, this.gameObject);
    }
    
    // public void SetElemToShowDefault()
    // {
    //     if (_type == BagElemType.Item)
    //         _bagUiController.ShowElem(_item, gameObject);
    //     else if (_type == BagElemType.Weapon)
    //         _bagUiController.ShowElem(_weapon, gameObject);
    //     else if (_type == BagElemType.Wingman)
    //         _bagUiController.ShowElem(_wingman, gameObject);
    //     
    //     Debug.Log("itemset: " + _item.Name);
    // }

    public void SetEdge()
    {
        if (toggle.isOn)
            pressedEdge.gameObject.SetActive(true);
        else 
            pressedEdge.gameObject.SetActive(false);
    }


    #region DragSupport
    
    public void OnDrag(PointerEventData eventData)
    {
        _dragItem.transform.position = eventData.position;
        _dragItem.transform.position += new Vector3(0, 0, 1f);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _dragItem.SetActive(true); 
        
        if (_type == BagElemType.Item)
            _dragItem.GetComponent<DragElem>().SetItem(gameObject, _sprite, BagElemType.Item, _item);
        else if (_type == BagElemType.Weapon)
            _dragItem.GetComponent<DragElem>().SetItem(gameObject, _sprite, BagElemType.Weapon, _weapon);
        else if (_type == BagElemType.Wingman)
            _dragItem.GetComponent<DragElem>().SetItem(gameObject, _sprite, BagElemType.Wingman, _wingman);
        else 
            throw new Exception("Elem type not match");
        
        _dragItem.GetComponent<DragElem>().isDrag = true;
        elemIcon.sprite = null;
        elemIcon.color = Color.clear;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _dragItem.SetActive(false);
        _dragItem.GetComponent<DragElem>().isDrag = false;
        elemIcon.sprite = _sprite;
        elemIcon.color = Color.white;

        _dragItem.GetComponent<DragElem>().OnEndDrag();
    }
    
    #endregion // DragSupport
}
