using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ability : MonoBehaviour
{
    [Header("Here's your header Alex")]
    public ChosenAbility ability1;
    PlayerMovement       playerMovement;
    PlayerStats          playerStats;
    
    [Header("Dark Arts"), SerializeField] DarkArtsVariables darkArtsVariables;
    List<EnemyBehaviour>                                    savedDarkArtsEnemies = new List<EnemyBehaviour>();
    List<Vector3>                                           darkArtsPositionPoints;
    
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (darkArtsVariables.isActive)
        {
            if (other.gameObject.GetComponent<DamageDealer>() && other.gameObject.GetComponent<DamageDealer>().IsAllied) Destroy(other.gameObject);
            else if (other.transform.CompareTag("Enemy"))
            {
                savedDarkArtsEnemies.Add(other.GetComponent<EnemyBehaviour>());
                AddToDarkArtsRenderer(other.transform);
                savedDarkArtsEnemies[^1].IsStunned = true;
            }
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
        [Header("GENERAL")]
        public BaseVariables baseVariables;
        
        /// <summary> ADDITIVE SUM </summary>
        public float timeActive;
        public bool  isActive;
        public float movementBoost;
        public int   damageMultiplier;
        
        [Header("GRAPHICS")]
        public LineRenderer   lineRenderer;
        public ParticleSystem activeParticles;

        public SpriteRenderer playerSprite;
        public Color          playerColorAtStart;
        public Color          playerColorWhileActive;

        public void ToggleAbility(bool enable)
        {
            baseVariables.canUseAbility = !enable;
            isActive                    = enable;
            playerSprite.color          = enable ? playerColorWhileActive : playerColorAtStart;
            if (enable) activeParticles.Play(); else activeParticles.Stop();
        }
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
        darkArtsVariables.ToggleAbility(true);
        
        playerMovement.MoveSpeed += Vector2.one * darkArtsVariables.movementBoost;
        
        //become gray

        yield return new WaitForSeconds(darkArtsVariables.timeActive);
        
        playerMovement.MoveSpeed -= Vector2.one * darkArtsVariables.movementBoost;

        yield return new WaitForSeconds(1);
        //after delay ; release and damage enemies, delete projectiles
        
        if(savedDarkArtsEnemies.Capacity > 0) foreach (var enemy in savedDarkArtsEnemies)
        {
            enemy.IsStunned = false;
            enemy.TakeDamage(playerStats.Damage * darkArtsVariables.damageMultiplier, enemy.transform.position);
            yield return new WaitForSeconds(0.02f);
        }
        
        darkArtsVariables.ToggleAbility(false);

        //LINE RENDERER
        StartCoroutine(DamageViaDarkArtsRenderer());
    }

    void AddToDarkArtsRenderer(Transform transform)
    {
        //darkArtsVariables.lineRenderer.positionCount++;
        //darkArtsVariables.lineRenderer.SetPosition(darkArtsVariables.lineRenderer.positionCount -1, transform.position);
        darkArtsPositionPoints.Capacity++;
        darkArtsPositionPoints[^1] = transform.position;

    }
    IEnumerator DamageViaDarkArtsRenderer()
    {
        //Vector3[] darkArtsPositionPoints = new Vector3[savedDarkArtsEnemies.Capacity - 1];
        //new Vector3[darkArtsVariables.lineRenderer.positionCount];
        
        foreach (var point in darkArtsPositionPoints)
        {
            //darkArtsPositionPoints = darkArtsPositionPoints.Skip(0).ToArray();
            
            //darkArtsVariables.lineRenderer.SetPosition(0, darkArtsPositionPoints[0]);
            //darkArtsVariables.lineRenderer.SetPosition(1, darkArtsPositionPoints[1]);
            //darkArtsVariables.lineRenderer.SetPosition(2, darkArtsPositionPoints[2]);
            
            darkArtsVariables.lineRenderer.SetPosition(0, point);
            //darkArtsVariables.lineRenderer.SetPosition(1, darkArtsPositionPoints.Find(point). );
            darkArtsVariables.lineRenderer.SetPosition(2, point);
            
            yield return new WaitForSeconds(0.3f);
        }
        
        darkArtsPositionPoints.Clear();
    }
    
    IEnumerator AoeAttack()
    {
        yield return new WaitForSeconds(1);
    }
#endregion
}
