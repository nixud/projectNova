using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PluginListControl : MonoBehaviour
{
    public GameObject pluginDocker;
    public List<Item> plugins;

    private int _listCount = 5;
    void Start()
    {


    }

    void Update()
    {
        
    }

    public void Init()
    {
        plugins = GameObject.Find("ItemControl").GetComponent<ItemControl>().plugins;

        if (plugins.Count <= _listCount)
        {
            foreach (var p in plugins)
            {
                CreatePerfab(p);
            }

            for (int i = 0; i < _listCount - plugins.Count; i++)
            {
                CreatePerfab(null);
            }
        }
        else
        {
            var rect = transform.GetComponentInChildren<RectTransform>();
            int index = 0;
            foreach (var p in plugins)
            {
                if (index > _listCount)
                    rect.offsetMin = new Vector2(rect.offsetMin.x, -120);
                CreatePerfab(p);
            }
        }
    }

    private void CreatePerfab(Item p)
    {
        var pluginTmp = Instantiate(pluginDocker, transform.GetChild(0));
        pluginTmp.transform.localScale = new Vector3(1, 1, 1);
        pluginTmp.GetComponent<PluginDocker>().InitPerfab(p);
    }
}
