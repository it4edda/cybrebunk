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
    void OnCollisionEnter(Collision other)
    {
        if (darkArtsVariables.isActive)
        {
            if (other.transform.CompareTag("Enemy"))
            {
                AddToDarkArtsRenderer(other.transform);
                other.gameObject.GetComponent<EnemyBehaviour>().IsStunned = true;
            }
            
        }
        //DO STUFF HERE, SHOULD WORK
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
        public bool           isActive;
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
        darkArtsVariables.isActive = true;
        Debug.Log("HAHA DID DARK ARTS");
        
        playerMovement.MoveSpeed += Vector2.one * darkArtsVariables.movementBoost;
        
        //become gray / particles
        darkArtsVariables.activeParticles.Play();
        //on collision ; stun enemies, stun projectiles

        yield return new WaitForSeconds(darkArtsVariables.timeActive);
        
        playerMovement.MoveSpeed            -= Vector2.one * darkArtsVariables.movementBoost;

        yield return new WaitForSeconds(1);
        //after delay ; release and damage enemies, delete projectiles
        //become normal color (racist)
        
        
        darkArtsVariables.activeParticles.Stop();

        //LINE RENDERER
        darkArtsVariables.isActive = false;
    }

    void AddToDarkArtsRenderer(Transform transform)
    {
        darkArtsVariables.lineRenderer.positionCount++;
        darkArtsVariables.lineRenderer.SetPosition(darkArtsVariables.lineRenderer.positionCount -1, transform.position);
        
    }
    
    IEnumerator AoeAttack()
    {
        yield return new WaitForSeconds(1);
    }
#endregion
}
