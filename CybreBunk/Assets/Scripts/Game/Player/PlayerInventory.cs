using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerInventory : MonoBehaviour
{
    #region Instance
    public static PlayerInventory instance;

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

    #region Variables
    PlayerStats stats;
    public List<ItemData> items;
    ItemData currentAbility;
    ItemData currentPattern;
    #endregion

    #region SetUp
    void Start()
    {
        stats = FindObjectOfType<PlayerStats>();
        items.Clear();
    }
    void OnEnable()
    {
        DamageDealer.OnHitEvent += DealingDamage;
    }

    void OnDisable()
    {
        DamageDealer.OnHitEvent -= DealingDamage;
    }
    #endregion

    #region Callers
    public void AddItem(ItemData newItem)
    {
        newItem.OnPickup(stats);
        //maybe convert to switch statement later
        if (newItem.itemType == ItemData.ItemType.Ability)
        {
            ChangeAbility(newItem);
        }else if (newItem.itemType == ItemData.ItemType.Pattern)
        {
            
        }
        else
        {
            items.Add(newItem);
        }
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
    #endregion
    
    #region Spawners
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
    #endregion

    #region Abilities
    void ChangeAbility(ItemData newAbility)
    {
        if (currentAbility)
        {
            ItemManager.instance.ReturnItem(currentAbility);
        }
        currentAbility = newAbility;
    }
    #endregion

    #region Pattern
    void ChangePattern(ItemData newPattern)
    {
        if (currentPattern)
        {
            ItemManager.instance.ReturnItem(currentPattern);
        }
        currentPattern = newPattern;
    }
    #endregion
}
