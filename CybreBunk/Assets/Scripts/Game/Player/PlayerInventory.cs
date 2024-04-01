using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory instance;
    PlayerStats stats;
    public List<ItemData> items;
    
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

    void Start()
    {
        stats = FindObjectOfType<PlayerStats>();
        items.Clear();

        DamageDealer.OnHitEvent += DealingDamage;
    }
    
    public void AddItem(ItemData newItem)
    {
        newItem.OnPickup(stats);
        items.Add(newItem);
        Debug.Log("Did effect" + newItem.itemName);
    }

    public void TakingDamage()
    {
        foreach (ItemData t in items)
        {
            t.OnTakeDamage(stats);
        }
    }
    
    public void DealingDamage(GameObject enemyBeingHit)
    {
        foreach (ItemData t in items)
        {
            t.OnDealDamage(stats);
        }
    }
}
