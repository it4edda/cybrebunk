using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class TarotData : ScriptableObject
{
    [Header("Menu")]
    [SerializeField] public GameObject tarotCard = default;
    [SerializeField] public string tarotDescription;

    [Header("Game")]
    [SerializeField] bool swordStart;
    [SerializeField] int startingHealth;
}
