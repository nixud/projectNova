using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipObject : MonoBehaviour
{
    private Item _equipment;

    private GameObject _pickButton;
    private Transform _player;

    private bool _inPickArea;
    private float _pickDistance;

    private void Start()
    {
        CreateEquipObject(1);
        _pickDistance = 5f;
    }

    void Update()
    {
        Move();
        if (Vector3.Distance(transform.position, _player.position) <= _pickDistance && !_inPickArea)
        {
            _inPickArea = true;
            _pickButton.GetComponent<PickButton>().SetEquipToPick(_equipment, this.gameObject);
            _pickButton.gameObject.SetActive(true);
        }
        else if (Vector3.Distance(transform.position, _player.position) > _pickDistance && _inPickArea)
        {
            _pickButton.GetComponent<PickButton>().SetEquipToPick(null, null);
            _inPickArea = false;
        }
    }

    public void CreateEquipObject(int itemNumber)
    {
        _equipment = ItemLoader.LoadData(itemNumber);
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(_equipment.PicPath);
        transform.localScale = new Vector3(3, 3, 3);
        _pickButton = GameObject.Find("PickButton");
        _player = GameObject.Find("Player").transform;
        _inPickArea = false;
    }

    public void Move()
    {
        
    }
}
