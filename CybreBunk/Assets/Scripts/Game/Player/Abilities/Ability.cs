using System;
using System.Collections;
using UnityEngine;

public class Ability : MonoBehaviour
{
    [Header("Here's your header Alex")]
    public ChosenAbility ability1;
    public ChosenAbility ability2;
    PlayerMovement       playerMovement;
    
    [Header("Dark Arts"), SerializeField] DarkArtsVariables  darkArtsVariables;
    [Header("AoeAttack"), SerializeField] AoeAttackVariables aoeAttackVariables;

    public enum ChosenAbility
    {
        DarkArts,
        AoeAttack,
        C
    }

    void Start()
    {
        playerMovement                  = GetComponent<PlayerMovement>();
        darkArtsVariables.canUseAbility = true;
    }

    public void InitiateAbility(ChosenAbility chosen)
    {
        switch (chosen)
        {
            case ChosenAbility.DarkArts:
                StartCoroutine(DarkArts());
                break;

            case ChosenAbility.AoeAttack:
                
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
    struct DarkArtsVariables
    {
        public bool         canUseAbility;
        public float        timeActive;
        public float        movementBoost; //ADDITIVE
        public LineRenderer lineRenderer;
    }
    [Serializable]
    struct AoeAttackVariables
    {
        public float      timeActive;
        public Collider2D damageCollider;
    }
#endregion
    IEnumerator DarkArts() //name of the item in isaac, im not THAT edgy
    {
        Debug.Log("HAHA DID DARK ARTS");

        if (!darkArtsVariables.canUseAbility) yield break;

        playerMovement.MoveSpeed += Vector2.one * darkArtsVariables.movementBoost;
        
        //become gray
        //on collision ; stun enemies, stun projectiles

        yield return new WaitForSeconds(1); //make variable for time
        
        playerMovement.MoveSpeed -= Vector2.one * darkArtsVariables.movementBoost;
        //after delay ; release and damage enemies, delete projectiles
        //become normal color (racist)


        //LINE RENDERER
    }
    IEnumerator AoeAttack()
    {
        yield return new WaitForSeconds(1);
    }
#endregion
}
