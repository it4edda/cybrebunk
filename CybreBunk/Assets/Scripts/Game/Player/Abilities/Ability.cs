using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Ability : MonoBehaviour
{
    [Header("Here's your header Alex")]
    [SerializeField] ChosenAbility ability1;
    [SerializeField] ChosenAbility ability2;
    PlayerMovement                 playerMovement;
    enum ChosenAbility
    {
        DarkArts,
        B,
        C
    }

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Initiate()
    {
        switch (ability1)
        {
            case ChosenAbility.DarkArts:
                DarkArts();
                break;

            case ChosenAbility.B:
                break;

            case ChosenAbility.C:
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }
    }

#region Vinushka
    void DarkArts() //name of the item in isaac, im not THAT edgy
    {
        //turn to Enumerator?
        //higher ms
        //become gray
        //on collision ; stun enemies, stun projectiles
        
        //after delay ; release and damage enemies, delete projectiles
        //become normal color (racist)
    }
#endregion
}
