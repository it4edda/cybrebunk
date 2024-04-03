using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemManager : MonoBehaviour
{
    #region Instance
    public static ItemManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public ItemPortrait itemPortrait;
    public List<ItemData> allItems;
    public GameObject itemCanvas;

    public Queue<ItemPortrait> itemPortraitQueue = new();
    PauseMenu pauseMenu;
    

    void Start()
    {
        pauseMenu = FindObjectOfType<PauseMenu>();
        ItemPortrait.ChosenItem += HasChosenItem;
    }

    public void SetItemSelect()
    {
        pauseMenu.TimeFreeze(true);
        itemCanvas.SetActive(true);
        while (itemPortraitQueue.Count <3)
        {
            ItemPortrait newPortrait = Instantiate(itemPortrait, itemCanvas.transform);
            itemPortraitQueue.Enqueue(newPortrait);
        }

        for (int i = 0; i < 3; i++)
        {
            ItemPortrait currentPortrait = itemPortraitQueue.Dequeue();
            currentPortrait.gameObject.SetActive(true);
            currentPortrait.SetUpItem();
        }
    }

    void HasChosenItem()
    {
        pauseMenu.TimeFreeze(false);
        itemCanvas.SetActive(false);
    }
    
    public ItemData GetRandomItem()
    {
        ItemData chosenItem = allItems[Random.Range(0, allItems.Count)];
        
        return chosenItem;
    }
}
