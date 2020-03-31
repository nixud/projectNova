
using UnityEngine;
using UnityEngine.UI;

// 设置道具栏道具
public class ItemUIControl : MonoBehaviour
{
    public ItemButton permanentItemButton;
    public ExpendableItemButton ExpendableItemButton;

    private Bag _bag;
    void Start()
    {
        _bag = Bag.Instance;
        permanentItemButton.SetItem(_bag.PermanentItemEquipped);
        ExpendableItemButton.SetItem(_bag.ExpendableItemEquipped, _bag.ExpendableItemCount);
    }
}
