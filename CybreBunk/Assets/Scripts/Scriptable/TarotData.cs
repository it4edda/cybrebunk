using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class TarotData : ScriptableObject
{
    [Header("General")]
    [SerializeField] public bool isPlayable;
    [SerializeField] public bool debugTool = false;
    
    
    [Header("Menu")]
    [SerializeField]        GameObject tarotCard = default;
    [SerializeField]        GameObject lockedCard;
    [SerializeField]        string     tarotDescription;
    public                  GameObject CurrentCard        => isPlayable ? tarotCard : lockedCard;
    public                  string     CurrentDescription => isPlayable ? tarotDescription : "Locked";

    [Header("Game"), SerializeField]
     public bool swordStart;
    [SerializeField] public float msStart;
    [SerializeField]        int   startingHealth;
}
