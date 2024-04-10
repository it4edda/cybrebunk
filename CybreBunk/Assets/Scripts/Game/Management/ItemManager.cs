
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
    [SerializeField] GameObject itemCanvas;

    public Queue<ItemPortrait> itemPortraitQueue = new();
    PauseMenu pauseMenu;
    

    void Start()
    {
        pauseMenu = FindObjectOfType<PauseMenu>();
    }

    void OnEnable()
    {
        ItemPortrait.ChosenItem += HasChosenItem;
    }

    void OnDisable()
    {
        ItemPortrait.ChosenItem -= HasChosenItem;
    }

    public void SetItemSelect()
    {
        FreezeAndCanvas(true);
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
        FreezeAndCanvas(false);
    }

    void FreezeAndCanvas(bool freeze)
    {
        if(!itemCanvas){Debug.Log("NO CANVAS AAGHAGHAHGAHGA"); return; }
        
        itemCanvas.SetActive(freeze);
        pauseMenu.TimeFreeze(freeze);
    }

    public ItemData GetRandomItem()
    {
        ItemData chosenItem = allItems[Random.Range(0, allItems.Count)];
        
        return chosenItem;
    }
}
