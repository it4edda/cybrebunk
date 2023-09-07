using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLogic : MonoBehaviour
{
    [SerializeField] ItemData itemData;
    void OnTriggerEnter2D(Collider2D other)
    {
        itemData.SetEffect();
        Destroy(gameObject);
    }
    //needs a popup on screen
    //ex: "YOU FOUND PILLS"
}