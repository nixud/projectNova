using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 永久性道具栏按钮
public class ItemButton : MonoBehaviour
{
    public Image Render;

    private Image thisImage;
    private Item item;
    // private Button button;

    private float cd;
    private float timer;
    private bool isUsingItem;

    void Awake()
    {
        // button = GetComponent<Button>();
        thisImage = GetComponent<Image>();
    }

    void Start()
    {
        // test
        // item = new Item();
        // item.EffectNumber = 1;
        // item.CD = 2f;
        // item.LoadEffect();
        // SetItem(item);
        
        Render.fillAmount = 0f;
    }

    void Update()
    {
        ItemFreeze();
    }

    public void SetItem(Item item)
    {
        if (item != null)
        {
            this.item = item;
            cd = this.item.CD;
            SetSprite();
        }
        else
            this.gameObject.SetActive(false);
    }

    private void SetSprite()
    {
        // ...
    }


    #region UseItem
    
    public void UsingItem()
    {
        if (!isUsingItem)
        {
            isUsingItem = true;
            StartCoroutine(Useitem());
        }
    }
    //计算道具cd
    IEnumerator Useitem()
    {
        item.Run();
        yield return new WaitForSeconds(cd);
        item.End();
    }
    
    // cd
    private void ItemFreeze()
    {
        if (isUsingItem)
        {
            timer += Time.deltaTime;
            Render.fillAmount = 1 - (timer / cd);
            if (cd - timer < 0.005f)
            {
                isUsingItem = false;
                timer = 0;
            }
        }
    }
    
    #endregion // UseItem
}
