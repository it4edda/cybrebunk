using System;
using UnityEngine;
using TMPro;

public class ItemPortrait : MonoBehaviour
{
    public static event Action ChosenItem;
    [SerializeField] SpriteRenderer itemSprite;
    [SerializeField] TextMeshProUGUI itemName;
    [SerializeField] TextMeshProUGUI itemDesc;

    ItemData currentItem;
    public void SetUpItem()
    {
        ItemPortrait.ChosenItem += Sleep;
        currentItem = FindObjectOfType<ItemManager>().GetRandomItem();
        if (currentItem.itemIcon)
        {
            itemSprite.sprite = currentItem.itemIcon;
        }

        itemName.text = currentItem.itemName;
        itemDesc.text = currentItem.itemDescription;
    }

    public void PickItem()
    {
        PlayerInventory.instance.AddItem(currentItem);
        Debug.Log("ChoseItem" + itemName);
        ChosenItem?.Invoke();
    }

    void Sleep()
    {
        ItemPortrait.ChosenItem -= Sleep;
        FindObjectOfType<ItemManager>().itemPortraitQueue.Enqueue(this);
        gameObject.SetActive(false);
    }
}
