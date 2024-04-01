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
        darkArtsVariables.baseVariables.canUseAbility = true;
    }

    public void InitiateAbility(ChosenAbility chosen)
    {
        switch (chosen)
        {
            case ChosenAbility.DarkArts:
                if (darkArtsVariables.baseVariables.canUseAbility) StartCoroutine(DarkArts());
                break;

            case ChosenAbility.AoeAttack:
                if (aoeAttackVariables.baseVariables.canUseAbility) StartCoroutine(AoeAttack());
                break;

            case ChosenAbility.C:
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }
    }

#region Vinushka
#region  Variables
    struct BaseVariables
    {
        public bool  canUseAbility;
        public float abilityCooldown;
    }
    
    [Serializable] struct DarkArtsVariables
    {
        public BaseVariables  baseVariables;
        public float          timeActive;
        public float          movementBoost; //ADDITIVE
        public LineRenderer   lineRenderer;
        public ParticleSystem activeParticles;
    }
    [Serializable] struct AoeAttackVariables
    {
        public BaseVariables baseVariables;  
        public float         timeActive;
        public Collider2D    damageCollider;
    }
#endregion
    IEnumerator DarkArts() //name of the item in isaac, im not THAT edgy
    {
        Debug.Log("HAHA DID DARK ARTS");
        
        playerMovement.MoveSpeed += Vector2.one * darkArtsVariables.movementBoost;
        
        //become gray / particles
        darkArtsVariables.activeParticles.Play();
        //on collision ; stun enemies, stun projectiles

        yield return new WaitForSeconds(1); //make variable for time
        
        playerMovement.MoveSpeed -= Vector2.one * darkArtsVariables.movementBoost;
        //after delay ; release and damage enemies, delete projectiles
        //become normal color (racist)

        yield return new WaitForSeconds(1);
        darkArtsVariables.activeParticles.Stop();

        //LINE RENDERER
    }

    void AddToRenderer(bool active)
    {
        if (active)
        {
            //darkArtsVariables.lineRenderer.SetPositions(++);
        }
        else
        {
            
        }
        
    }
    
    IEnumerator AoeAttack()
    {
        yield return new WaitForSeconds(1);
    }
#endregion
}
