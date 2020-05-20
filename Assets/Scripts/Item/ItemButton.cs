using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
//using System.Runtime.Remoting;
using UnityEngine;
using UnityEngine.UI;

// 装备道具使用按钮
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

    private void Awake()
    {
        thisImage = GetComponent<Image>();
    }

    private void Start()
    {
        UsingItemFreeze.gameObject.SetActive(false);
        ChangeCount();
        ChangeSprite();
    }

    private void Update()
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

    /// <summary>
    /// 设置当前显示道具
    /// </summary>
    /// <param name="itemStatus">道ItemStatus</param>
    public void SetItem(ItemStatus itemStatus)
    {
        itemTemp = itemStatus;
        itemChanged = true;
    }
    
    /// <summary>
    /// 切换道具
    /// </summary>
    /// <param name="itemStatus"></param>
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
    
    /// <summary>
    /// 道具效果时间
    /// </summary>
    /// <returns></returns>
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
    
    /// <summary>
    /// 道具使用后冷却cd计算
    /// </summary>
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

    public void OnEnd()
    {
        if (isUsingItem)
        {
            StopCoroutine(Useitem());
            itemStatus.item.End();
            isUsingItem = false;
            shouldFreeze = true;
            shouldChangeItem = true;
            if (type == ItemType.Consume && nowEffectCount == 0)
                transform.GetComponentInParent<ItemControl>().ChangeEquipment();
            UsingItemFreeze.gameObject.SetActive(false);
        }
    }
    
    // 充能
    public void GetAccumulation(int acc)
    {
        if (type == ItemType.Accumulate && !isUsingItem)
            nowAccumulate += acc;
        if (nowAccumulate >= itemAccumulate)
            nowAccumulate = itemAccumulate;
    }
    
    // 切换道具暂存状态
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

    // 更新消耗型道具次数
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
