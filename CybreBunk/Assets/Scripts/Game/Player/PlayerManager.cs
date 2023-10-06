using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

static class PlayerManager
{
    public static TarotData selectedCard = FindTarotCard();
    public static void      SwapCard(TarotData card) => selectedCard = card;

    //is currently in DamageDealer
    public static TarotData FindTarotCard() => selectedCard ? selectedCard : ScriptableObject.CreateInstance<TarotData>();
}
