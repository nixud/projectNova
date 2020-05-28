using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DeleteButtonPointerEnter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image buttonImage;
    public Color normalColor, pressedColor;
    private GameObject _bagController;
    private GameObject _dragItem;
    private bool _isEnter;

    private bool _interactableTemp;
    void Start()
    {
        _bagController = GameObject.Find("EquipCanvas");
        _dragItem = _bagController.GetComponent<BagUIController>().DragHelper;
    }

    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_dragItem.GetComponent<DragElem>().isDrag)
            return;
        _isEnter = true;
        _interactableTemp = GetComponent<Button>().interactable;
        GetComponent<Button>().interactable = true;
        buttonImage.color = pressedColor;
        _dragItem.GetComponent<DragElem>().onEndDragEvent += Delete;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!_isEnter)
            return;
        _isEnter = false;
        GetComponent<Button>().interactable = _interactableTemp;
        buttonImage.color = normalColor;
        try
        {
            _dragItem.GetComponent<DragElem>().onEndDragEvent -= Delete;
        }
        catch (Exception e)
        {
            // ignore
        }
    }

    private void Delete()
    {
        _bagController.GetComponent<BagUIController>().Delete(_dragItem);
    }
}
