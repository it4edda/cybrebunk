using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemPortrait : MonoBehaviour
{
    public static event Action ChosenItem;
    [SerializeField] Image itemSprite;
    [SerializeField] TextMeshProUGUI itemName;
    [SerializeField] TextMeshProUGUI itemDesc;

    ItemData currentItem;

    bool pickedThisItem;
    public void SetUpItem()
    {
        pickedThisItem = false;
        currentItem = ItemManager.instance.GetRandomItem();
        if (currentItem.itemIcon)
        {
            itemSprite.sprite = currentItem.itemIcon;
        }

        itemName.text = currentItem.itemName;
        itemDesc.text = currentItem.itemDescription;
    }

    void OnEnable()
    {
        ItemPortrait.ChosenItem += Sleep;
    }

    void OnDisable()
    {
        ItemPortrait.ChosenItem -= Sleep;
    }

    public void PickItem()
    {
        pickedThisItem = true;
        PlayerInventory.instance.AddItem(currentItem);
        Debug.Log("ChoseItem" + itemName.text);
        ChosenItem?.Invoke();
    }

    void Sleep()
    {
        if (currentItem.itemType == ItemData.ItemType.Ability && !pickedThisItem)
        {
            ItemManager.instance.ReturnItem(currentItem);
        }
        if (currentItem.itemType == ItemData.ItemType.Pattern && !pickedThisItem)
        {
            ItemManager.instance.ReturnItem(currentItem);
        }
        ItemManager.instance.itemPortraitQueue.Enqueue(this);
        this.gameObject.SetActive(false);
    }
}
