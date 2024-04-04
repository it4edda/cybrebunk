using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemVisual : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static event Action<ItemData, PointerEventData, GameObject> OnHover;
    public static event Action<ItemData, GameObject> OnUnHover;
    
    [SerializeField] Image itemIcon;
    
    ItemData currentItem;
    PauseMenu pauseMenu;

    void Start()
    {
        pauseMenu = FindObjectOfType<PauseMenu>();
    }

    void OnEnable()
    {
        PauseMenu.pausing += ReturnToQueue;
    }

    void OnDisable()
    {
        PauseMenu.pausing -= ReturnToQueue;
    }

    public void SetUpVisual(ItemData item)
    {
        currentItem = item;
        if(!currentItem.itemIcon){return;}
        itemIcon.sprite = currentItem.itemIcon;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnHover?.Invoke(currentItem, eventData, gameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnUnHover?.Invoke(currentItem, gameObject);
    }

    void ReturnToQueue(bool isPausing)
    {
        if (isPausing) { return; }
        
        pauseMenu.visualsQueue.Enqueue(this);
        gameObject.SetActive(false);
    }
}
