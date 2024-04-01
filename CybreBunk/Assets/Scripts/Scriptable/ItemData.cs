using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class ItemData : ScriptableObject
{
    #region Variables

    [Header("General")] public string itemName;
    public string itemDescription;
    public Sprite itemIcon;

    public List<ItemEffect> itemEffects = new();

    #endregion

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
        public int attackSpeedIncrease;
        public int damageIncrease;
        public float movementSpeedIncrease;
    }

    public enum ActivationConditions
    {
        Pickup,
        TakeDamage,
        DealDamage
    }

    #endregion

    #region CallFunktions
    public void OnPickup(PlayerStats playerStats)
    {
        foreach (ItemEffect itemEffect in itemEffects.Where(effect =>
                     effect.activationConditions == ActivationConditions.Pickup))
        {
            UpdateStats(playerStats, itemEffect);
        }
    }

    public void OnTakeDamage(PlayerStats playerStats)
    {
        foreach (ItemEffect itemEffect in itemEffects.Where(effect =>
                     effect.activationConditions == ActivationConditions.TakeDamage))
        {
            UpdateStats(playerStats, itemEffect);
        }
    }

    public void OnDealDamage(PlayerStats playerStats)
    {
        foreach (ItemEffect itemEffect in itemEffects.Where(effect =>
                     effect.activationConditions == ActivationConditions.DealDamage))
        {
            UpdateStats(playerStats, itemEffect);
        }
    }
    #endregion

    void UpdateStats(PlayerStats playerStats, ItemEffect effect)
    {
        playerStats.MaxHealth += effect.stats.maxHealth;
        playerStats.Health += effect.stats.health;
        playerStats.Damage += effect.stats.damageIncrease;
        playerStats.MovementSpeed += effect.stats.movementSpeedIncrease;
    }
}
