using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PluginDocker : MonoBehaviour
{
    public Image icon;
    public Text pluginName;
    public Text description;

    private Item _plugin;
    private string _defaultIconPath;
    void Start()
    {
    }

    private void Update()
    {
    }

    public void RemoveThis()
    {
        GameObject.Find("ItemControl").GetComponent<ItemControl>().DeletePlugin(_plugin);
        Destroy(this.gameObject);
    }

    public void InitPerfab(Item plugin)
    {
        _plugin = plugin;

        if (_plugin == null)
        {
            icon.sprite = Resources.Load<Sprite>(_defaultIconPath);
            pluginName = null;
            description = null;
        }
        else
        {
            icon.sprite = Resources.Load<Sprite>(_plugin.PicPath);
            pluginName.text = _plugin.Name;
            description.text = _plugin.Description;
        }
    }
}
