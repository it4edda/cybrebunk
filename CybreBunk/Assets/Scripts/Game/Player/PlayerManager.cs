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

static class ItemManager
{
    [SerializeField] static ItemData[] items;
    static ItemData[] Items
    {
        get => GetItems();
        set => throw new System.NotImplementedException();
    }
    static ItemData[] GetItems()
    {
        return null;
        //FIX THIS
        //https://discussions.unity.com/t/how-can-i-find-all-instances-of-a-scriptable-object-in-the-project-editor/198002/3
    }
        
}