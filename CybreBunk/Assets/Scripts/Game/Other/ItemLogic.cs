using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLogic : Interaction
{
    [Header("Item Exclusive")]
    [SerializeField] ItemData itemData;

    protected override void InteractionActive()
    {
        itemData.SetEffect(gameObject);
        base.InteractionActive();
        Destroy(gameObject);
        
    }
    //needs a popup on screen
    //ex: "YOU FOUND PILLS"
}