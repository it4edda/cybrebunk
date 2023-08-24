using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class PlayerManager
{
    public static TarotData selectedCard;
    public static void      SwapCard(TarotData card) => selectedCard = card;
}
