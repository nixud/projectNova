using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 消耗性道具栏按钮
public class ExpendableItemButton : MonoBehaviour
{
    public Image Render;
    public Text itemCount;

    private Image thisImage;
    private Item item;
    private Button button;

    private float cd;
    private float timer;
    private bool isUsingItem;
    private int count;
    void Awake()
    {
        button = GetComponent<Button>();
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

    public void SetItem(Item item, int count = 0)
    {
        if (item != null)
        {
            this.item = item;
            cd = this.item.CD;
            this.count = count;
            itemCount.text = count.ToString();
        }
        else
            gameObject.SetActive(false);
    }


    #region UseItem
    
    public void UsingItem()
    {
        if (!isUsingItem && count > 0)
        {
            isUsingItem = true;
            StartCoroutine(Useitem());

            count--;
            itemCount.text = count.ToString();
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
        if (count == 0)
            Render.fillAmount = 1f;
        else if (isUsingItem)
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
