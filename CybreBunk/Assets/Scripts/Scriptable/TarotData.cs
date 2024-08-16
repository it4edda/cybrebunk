using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class TarotData : ScriptableObject
{
    [Header("General")]
    [SerializeField] public bool isPlayable;
    [SerializeField] public bool debugTool = false;


    [Header("Menu")]
    //[SerializeField] public GameObject tarotCard = default;
    //[SerializeField]        GameObject lockedCard;
    [SerializeField] public int totalWins;
    [SerializeField]        string     tarotDescription;
    public string     CurrentDescription => isPlayable ? tarotDescription : "Locked";

    [Header("Menu Sprites")]
    [SerializeField] Sprite unlockedCard;
    [SerializeField] Sprite     lockedCard;
    public           Sprite CurrentCard => isPlayable ? unlockedCard : lockedCard;
     
    [Header("Game"), SerializeField]
     public bool swordStart;
    [SerializeField] public float startingAttackSpeed   = 0.2f;
    [SerializeField] public float bulletSpeed           = 0.69f;
    [SerializeField] public float playerRange        = 1;
    [SerializeField] public int   startingDamage        = 1;
    [SerializeField] public float startingMovementSpeed = 1.5f;
    [SerializeField] public int   startingHealth        = 5;
}
