using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;


// 不清楚背包准备如何实现，暂时先实现道具栏需要功能
public class Bag
{
    // 单例
    private Bag()
    {
        PermanentItemEquipped = null;
        ExpendableItemEquipped = null;
        ExpendableItemCount = 0;
    }
    private static Bag _instance;
    public static Bag Instance
    {
        get
        {
            if (_instance == null)
                _instance = new Bag();
            return _instance;
        }
    }
 
    // 已装备永久道具
    public Item PermanentItemEquipped;
    // 已装备消耗道具
    public Item ExpendableItemEquipped;
    public int ExpendableItemCount;
    
    private List<itemCount> itemList; 
    
    public void Init()
    {
        
    }

    
    

    private struct itemCount
    {
        public int count;
        public Item item;

        public itemCount(Item item, int count)
        {
            this.count = count;
            this.item = item;
        }
    }
}
