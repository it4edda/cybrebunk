/*
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Obsolete]
[CreateAssetMenu(fileName = "New Item", menuName = "Items/Attack")]
public class AttackItemData : ItemData, IDamageStats
{
    [Header("Attack Exclusive")]
    [SerializeField] int attackSpeedIncrease;
    [SerializeField] int damageIncrease;

    
    public int AttackSpeedIncrease
    {
        get => attackSpeedIncrease;
        set => attackSpeedIncrease = value;
    }
    public int DamageIncrease
    {
        get => damageIncrease; 
        set => damageIncrease = value;
    }
    protected override void SetEffect()
    {
        stats.Damage += DamageIncrease;
        
        //add attack speed variable;
        Debug.Log(stats.Damage);
        base.SetEffect();
    }

}

public interface IDamageStats
{
    int AttackSpeedIncrease  { get; set; }
    int DamageIncrease { get; set; }
}
*/


