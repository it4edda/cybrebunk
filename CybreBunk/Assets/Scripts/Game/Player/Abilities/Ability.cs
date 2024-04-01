using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class Ability : MonoBehaviour
{
    [Header("Here's your header Alex")]
    public ChosenAbility ability1;
    public ChosenAbility ability2;
    PlayerMovement       playerMovement;
    PlayerStats          playerStats;
    
    [Header("Dark Arts"), SerializeField] DarkArtsVariables  darkArtsVariables;
    EnemyBehaviour[]                                         savedDarkArtsEnemies;
    
    [Header("AoeAttack"), SerializeField] AoeAttackVariables aoeAttackVariables;

    public enum ChosenAbility
    {
        DarkArts,
        AoeAttack,
        C
    }

    void Start()
    {
        playerStats                                   = FindObjectOfType<PlayerStats>();
        playerMovement                                = GetComponent<PlayerMovement>();
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
            if (other.gameObject.GetComponent<DamageDealer>().IsAllied) Destroy(other.gameObject);
            else if (other.transform.CompareTag("Enemy"))
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
        public BaseVariables baseVariables;
        
        public bool           isActive;
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
        
        //become gray
        darkArtsVariables.activeParticles.Play();

        yield return new WaitForSeconds(darkArtsVariables.timeActive);
        
        playerMovement.MoveSpeed            -= Vector2.one * darkArtsVariables.movementBoost;

        yield return new WaitForSeconds(1);
        //after delay ; release and damage enemies, delete projectiles
        
        Debug.Log("DU JOBBADE HÄR SENAST WILLIAM LYCKA TILL HAHAHAHAHAHAHHAHAHA");
        if(savedDarkArtsEnemies.Length > 0) foreach (var enemy in savedDarkArtsEnemies)
        {
            enemy.IsStunned = false;
            enemy.TakeDamage(playerStats.Damage, enemy.transform.position);
            yield return new WaitForSeconds(0.02f);
        }
        
        darkArtsVariables.activeParticles.Stop();

        //LINE RENDERER
        
        darkArtsVariables.isActive = false;
        StartCoroutine(DamageViaDarkArtsRenderer());
    }

    void AddToDarkArtsRenderer(Transform transform)
    {
        darkArtsVariables.lineRenderer.positionCount++;
        darkArtsVariables.lineRenderer.SetPosition(darkArtsVariables.lineRenderer.positionCount -1, transform.position);
        
    }
    IEnumerator DamageViaDarkArtsRenderer()
    {
        Vector3[] points = new Vector3[darkArtsVariables.lineRenderer.positionCount];
        //PLAY SOUND
        foreach (var point in points)
        {
            points = points.Skip(0).ToArray();
            yield return new WaitForSeconds(0.1f);
        }
    }
    
    IEnumerator AoeAttack()
    {
        yield return new WaitForSeconds(1);
    }
#endregion
}
