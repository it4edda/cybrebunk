using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemLogic : Interaction
{
    [Header("Item Exclusive")]
    [SerializeField] ItemData itemData;
    PlayerStats stats;
    

    protected override void InteractionActive()
    {
        SetEffect(gameObject);
        base.InteractionActive();
        Destroy(gameObject);
    }
    void SetEffect(GameObject a) // move this to ItemLogic instead, nils
    {
        //stats.movement += movementSpeedIncrease;
        stats        =  FindObjectOfType<PlayerStats>();
        stats.Damage += itemData.attackExclusive.damageIncrease;
        //attack speed
        
        //subscribe to unity event
        Debug.Log("Did Card Effect");
    }
    void OnDisable()
    {
        DamageDealer.OnHitEvent -= SetEffect;
    }
    //needs a popup on screen
    //ex: "YOU FOUND PILLS"
}