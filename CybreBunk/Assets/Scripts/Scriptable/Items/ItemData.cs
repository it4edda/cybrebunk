using UnityEngine;

public class ItemData : ScriptableObject
{
    #region Variables
    [Header("General")] public string itemName;
    public string itemDescription;
    public Sprite itemIcon;
    //public Image itemIcon;
    #endregion
    
    public enum ActivationConditions
    {
        Pickup,
        TakeDamage,
        DealDamage
    }

    #region CallFunktions
    public virtual void OnPickup(PlayerStats playerStats)
    {
        
    }

    public virtual void OnTakeDamage(PlayerStats playerStats)
    {
        
    }

    public virtual void OnDealDamage(PlayerStats playerStats)
    {
        
    }
    #endregion
}
