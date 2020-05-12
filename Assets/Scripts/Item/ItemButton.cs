using System;
using System.Collections;
using System.Collections.Generic;
//using System.Runtime.Remoting;
using UnityEngine;
using UnityEngine.UI;

// 永久性道具栏按钮
public class ItemButton : MonoBehaviour
{
    public Image Render;
    public Image Condition;
    public Image UsingItemFreeze;
    public Text Count;

    private Image thisImage;
    private ItemStatus itemStatus;

    private ItemStatus itemTemp;
    private bool itemChanged;
    private bool shouldChangeItem = true;

    private float timer;
    private bool isUsingItem;
    private bool shouldFreeze;

    private ItemType? type = null;
    
    // 道具属性
    private float effectTime;
    
    private int itemAccumulate;
    private int nowAccumulate;

    private int nowEffectCount;
    private float itemCd;

    void Awake()
    {
        thisImage = GetComponent<Image>();
    }

    void Start()
    {
        UsingItemFreeze.gameObject.SetActive(false);
        ChangeCount();
        ChangeSprite();
    }

    void Update()
    {
        ItemFreeze();
        CheckoutCondition();
        ChangeItem(itemTemp);
    }

    private void FixedUpdate()
    {
        if (isUsingItem)
            itemStatus.item.Update();
    }

    public void SetItem(ItemStatus itemStatus)
    {
        itemTemp = itemStatus;
        itemChanged = true;
    }
    
    public void ChangeItem(ItemStatus itemStatus)
    {
        if (!itemChanged || !shouldChangeItem)
            return;
        Render.fillAmount = 0f;
        this.itemStatus = itemStatus;
        if (itemStatus != null)
        {
            this.type = itemStatus.item.Type;
            effectTime = itemStatus.item.ItemEffects.time;
            if (type == ItemType.Accumulate)
            {
                itemAccumulate = itemStatus.item.Accumulate;
                nowAccumulate = itemStatus.accumulate;

                shouldFreeze = true;
            }
            else
            {
                itemCd = itemStatus.item.Cd;
                nowEffectCount = itemStatus.effectCount;

                shouldFreeze = false;
                Render.fillAmount = 0f;
            }
            
        }
        else
        {
            Init();
        }
        ChangeCount();
        ChangeSprite();
        itemChanged = false;
    }
    

    #region UseItem
    
    public void UsingItem()
    {
        if (!isUsingItem && !shouldFreeze && !Condition.IsActive() && itemStatus != null)
        {
            isUsingItem = true;
            StartCoroutine(Useitem());
            
            if (type == ItemType.Accumulate)
            {
                nowAccumulate = 0;
            }
            else
            {
                nowEffectCount--;
                ChangeCount();
            }
        }
    }
    //计算道具cd
    IEnumerator Useitem()
    {
        UsingItemFreeze.gameObject.SetActive(true);
        shouldChangeItem = false;
        itemStatus.item.Run();
        
        yield return new WaitForSeconds(effectTime);
        
        itemStatus.item.End();
        isUsingItem = false;
        shouldFreeze = true;
        shouldChangeItem = true;
        if (type == ItemType.Consume && nowEffectCount == 0)
            transform.GetComponentInParent<ItemControl>().ChangeEquipment();
        UsingItemFreeze.gameObject.SetActive(false);
    }
    
    // cd
    private void ItemFreeze()
    {
        if (shouldFreeze)
        {
            if (type == ItemType.Accumulate)
            {
                Render.fillAmount = (float)(itemAccumulate - nowAccumulate) / itemAccumulate;
                if (itemAccumulate == nowAccumulate)
                    shouldFreeze = false;
            }
            else if (type == ItemType.Consume)
            {
                timer += Time.deltaTime;
                Render.fillAmount = 1 - (timer / itemCd);
                if (itemCd - timer < 0.00005f)
                {
                    shouldFreeze = false;
                    timer = 0;
                }
            }
        }
    }

    private void CheckoutCondition()
    {
        if (itemStatus != null)
            Condition.gameObject.SetActive(!itemStatus.item.ItemEffects.Condition());
        else
            Condition.gameObject.SetActive(false);
    }
    
    #endregion // UseItem

    public void GetAccumulation(int acc)
    {
        if (type == ItemType.Accumulate && !isUsingItem)
            nowAccumulate += acc;
        if (nowAccumulate >= itemAccumulate)
            nowAccumulate = itemAccumulate;
    }
    
    public void SaveItemStatus()
    {
        if (type == ItemType.Accumulate)
            itemStatus.accumulate = nowAccumulate;
        else if (type == ItemType.Consume)
            itemStatus.effectCount = nowEffectCount;
    }
    
    private void ChangeSprite()
    {
        if (itemStatus == null)
            thisImage.sprite = Resources.Load<Sprite>(@"ItemButton/ItemButton");
        else
            thisImage.sprite = Resources.Load<Sprite>(itemStatus.item.PicPath);
    }

    private void ChangeCount()
    {
        if (type == ItemType.Consume)
            Count.text = nowEffectCount.ToString();
        else
            Count.text = null;
    }

    private void Init()
    {
        UsingItemFreeze.gameObject.SetActive(false);
        timer = 0f;
        isUsingItem = false;
        shouldFreeze = false;
        type = null;
        effectTime = 0f;
        itemAccumulate = 0;
        nowAccumulate = 0;
        nowEffectCount = 0;
        itemCd = 0f;
    }
}
