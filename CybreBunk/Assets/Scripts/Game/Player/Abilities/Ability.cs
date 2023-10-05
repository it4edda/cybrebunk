using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class Ability : MonoBehaviour
{
    [Header("Here's your header Alex")]
    [SerializeField] ChosenAbility ability1;
    [SerializeField] ChosenAbility ability2;
    PlayerMovement                 playerMovement;

    [Header("Vinushka")]
    [SerializeField] DarkArtsVariables darkArtsVariables;
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
                StartCoroutine(DarkArts());
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
#region  variables
    [Serializable]
    struct Vinushka
    {
        DarkArtsVariables darkArtsVariables;
    }
    struct DarkArtsVariables
    {
        public float        timeActive;
        public LineRenderer lineRenderer;
    }
#endregion
    IEnumerator DarkArts() //name of the item in isaac, im not THAT edgy
    {
        DarkArtsVariables darkArtsLineVariables;
        
        
        //higher ms
        //become gray
        //on collision ; stun enemies, stun projectiles

        yield return new WaitForSeconds(1); //make variable for time
        
        //after delay ; release and damage enemies, delete projectiles
        //become normal color (racist)
        
        
        //LINE RENDERER
    }
    //ADD SCRIPT "PLAYER INTERACTION"
#endregion
}
