using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class ItemData : ScriptableObject
{
#region Variables
    [Header("General")]
    public string itemName;
    public string itemDescription;
    public Sprite itemIcon;

    public MovementExclusive movementExclusive;
    public AttackExclusive attackExclusive;
    
    PlayerStats stats;
#endregion

#region Structs 
    //could be placed outside of class if needed
    [Serializable]
    public struct AttackExclusive
    {
        public int attackSpeedIncrease;
        public int damageIncrease;
    }
    [Serializable]
    public struct MovementExclusive
    {
        public float movementSpeedIncrease;
    }
#endregion
}
