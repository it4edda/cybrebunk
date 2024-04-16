using System;
using UnityEngine;
using TMPro;
using UnityEditor.Search;
using UnityEngine.UI;

public class ItemPortrait : MonoBehaviour
{
    public static event Action ChosenItem;
    [SerializeField] Image itemSprite;
    [SerializeField] TextMeshProUGUI itemName;
    [SerializeField] TextMeshProUGUI itemDesc;

    ItemData currentItem;
    public void SetUpItem()
    {
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
        PlayerInventory.instance.AddItem(currentItem);
        Debug.Log("ChoseItem" + itemName.text);
        ChosenItem?.Invoke();
    }

    void Sleep()
    {
        ItemManager.instance.itemPortraitQueue.Enqueue(this);
        this.gameObject.SetActive(false);
    }
}
