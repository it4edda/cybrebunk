using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

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
        Debug.Log("Dealing Damage");
        foreach (ItemData t in items)
        {
            t.OnDealDamage(stats);
        }
    }

    public void StartSpawning(PlayerStats playerStats, SpawnableItems.SpawnableObjects spawnableObjects)
    {
        StartCoroutine(SpawnObject(playerStats, spawnableObjects));
    }
    
    IEnumerator SpawnObject(PlayerStats playerStats, SpawnableItems.SpawnableObjects spawnableObjects)
    {
        switch (spawnableObjects.spawnPos)
        {
            case SpawnableItems.SpawnPos.OnPlayer:
                Instantiate(spawnableObjects.objectToSpawn, playerStats.transform.position, quaternion.identity);
                break;
            case SpawnableItems.SpawnPos.Radius:
                Vector2 point = Random.insideUnitCircle.normalized * Random.Range(spawnableObjects.minRadius, spawnableObjects.maxRadius);

                Instantiate(spawnableObjects.objectToSpawn, point, quaternion.identity);
                break;
            default:
                break;
        }
        yield return new WaitForSeconds(spawnableObjects.timeBetweenSpawns);

        if (spawnableObjects.spawnRepeatedly)
        {
            StartCoroutine(SpawnObject(playerStats, spawnableObjects));
        }
    }
}
