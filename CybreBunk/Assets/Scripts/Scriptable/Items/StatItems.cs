using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/StatItem", fileName = "NewStatItem")]
public class StatItems : ItemData
{
    public List<ItemEffect> itemEffects = new();

    #region Structs and Enums
    [Serializable]
    public struct ItemEffect
    {
        public ActivationConditions activationConditions;
        public Stats stats;
    }

    //could be placed outside of class if needed
    [Serializable]
    public struct Stats
    {
        public int maxHealth;
        public int health;
        public float range;
        public int attackSpeedIncrease;
        public int damageIncrease;
        public float movementSpeedIncrease;
        public int increaseInBloodGain;
    }

    
    #endregion

    #region CallFunktion

    public override void OnPickup(PlayerStats playerStats)
    {
        foreach (ItemEffect itemEffect in itemEffects.Where(effect =>
                     effect.activationConditions == ActivationConditions.Pickup))
        {
            UpdateStats(playerStats, itemEffect);
        }
    }

    public override void OnDealDamage(PlayerStats playerStats)
    {
        foreach (ItemEffect itemEffect in itemEffects.Where(effect =>
                     effect.activationConditions == ActivationConditions.DealDamage))
        {
            Debug.Log(itemEffect);
            UpdateStats(playerStats, itemEffect);
        }
    }

    public override void OnTakeDamage(PlayerStats playerStats)
    {
        foreach (ItemEffect itemEffect in itemEffects.Where(effect =>
                     effect.activationConditions == ActivationConditions.TakeDamage))
        {
            UpdateStats(playerStats, itemEffect);
        }
    }

    #endregion

    //ADD RANGE
    void UpdateStats(PlayerStats playerStats, ItemEffect effect)
    {
        playerStats.MaxHealth += effect.stats.maxHealth;
        playerStats.Health += effect.stats.health;
        playerStats.Range += effect.stats.range;
        playerStats.IncreaseInBloodGain += effect.stats.increaseInBloodGain;
        playerStats.Damage += effect.stats.damageIncrease;
        playerStats.MovementSpeed += effect.stats.movementSpeedIncrease;
    }
}
