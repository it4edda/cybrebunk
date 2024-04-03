using UnityEngine;
using UnityEngine.UI;

public class ItemVisual : MonoBehaviour
{
    [SerializeField] Image itemIcon;
    
    ItemData currentItem;

    public void SetUpVisual(ItemData item)
    {
        currentItem = item;
        if(!currentItem.itemIcon){return;}
        itemIcon.sprite = currentItem.itemIcon;
    }
}
