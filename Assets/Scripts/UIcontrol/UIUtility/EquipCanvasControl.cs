using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipCanvasControl : MonoBehaviour
{
    public GameObject equips;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnClick()
    {
        Time.timeScale = 0;
        equips.SetActive(true);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
