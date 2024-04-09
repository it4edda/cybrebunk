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
    //List<Vector3>                                           darkArtsPositionPoints;
    
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
        darkArtsVariables.activeParticles.Stop();
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
        public Animator      slasher;
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
        
        StartCoroutine(DarkArtsDamageAndRenderer());
    }

    
    IEnumerator DarkArtsDamageAndRenderer()
    {
        darkArtsVariables.isActive = false;
        int localCount = 0;
        darkArtsVariables.lineRenderer.positionCount = 3;
        
        List<Vector2> enemyPositions = new List<Vector2>();
        foreach (var enemy in savedDarkArtsEnemies)
        {
            enemyPositions.Add(enemy.transform.position);
        }
        //THIS IS UN-GPTd CODE
        /*foreach (var enemy in savedDarkArtsEnemies)
        {
            darkArtsVariables.lineRenderer.SetPosition(0, enemyPositions[localCount]);
            if (localCount -1 > 0) darkArtsVariables.lineRenderer.SetPosition(1, enemyPositions[localCount - 1]);
            else darkArtsVariables.lineRenderer.SetPosition(1,                  enemyPositions[localCount]);
            if (localCount -2 > 0) darkArtsVariables.lineRenderer.SetPosition(2, enemyPositions[localCount - 2]);
            else darkArtsVariables.lineRenderer.SetPosition(2,                   enemyPositions[localCount]);
            
            yield return new WaitForSeconds(0.05f);

            if (enemy != null)
            {
                enemy.IsStunned = false;
                enemy?.TakeDamage(playerStats.Damage * darkArtsVariables.damageMultiplier, enemy.transform.position);
            }
            localCount++;
        }*/
        //THIS IS GPT
        foreach (var enemy in savedDarkArtsEnemies)
        {
            darkArtsVariables.lineRenderer.SetPosition(0, enemyPositions[localCount]);
            darkArtsVariables.lineRenderer.SetPosition(1, enemyPositions[Math.Max(localCount - 1, 0)]);
            darkArtsVariables.lineRenderer.SetPosition(2, enemyPositions[Math.Max(localCount - 2, 0)]);
            
            yield return new WaitForSeconds(0.05f);

            if (enemy != null)
            {
                enemy.IsStunned = false;
                enemy?.TakeDamage(playerStats.Damage * darkArtsVariables.damageMultiplier, enemy.transform.position);
            }
            localCount++;
        }//END OF GPT

        for (int i = 0; i < 3; i++)
        {
            //darkArtsVariables.lineRenderer.SetPosition(0, darkArtsVariables.lineRenderer.GetPosition(1));
            //darkArtsVariables.lineRenderer.SetPosition(1, darkArtsVariables.lineRenderer.GetPosition(2));
            //darkArtsVariables.lineRenderer.SetPosition(2, transform.position);
            darkArtsVariables.lineRenderer.SetPosition(i,transform.position);
            yield return new WaitForSeconds(0.05f);
        }

        darkArtsVariables.lineRenderer.positionCount = 0;
        savedDarkArtsEnemies.Clear();
        darkArtsVariables.ToggleAbility(false);
    }
    
    IEnumerator AoeAttack()
    {
        aoeAttackVariables.baseVariables.canUseAbility = false;
        aoeAttackVariables.slasher.gameObject.SetActive(true);
        //Vector3 a = weaponGraphics.localScale;
        //weaponGraphics.localScale = new Vector3(a.x, a.y * -1, a.z);
        yield return new WaitForSeconds(aoeAttackVariables.slasher.GetCurrentAnimatorStateInfo(0).length);
        aoeAttackVariables.slasher.gameObject.SetActive(false);
        aoeAttackVariables.baseVariables.canUseAbility = true;
        
    }
#endregion
}






//darkArtsVariables.lineRenderer.SetPosition(1, savedDarkArtsEnemies[localCount +1 ].transform.position);
//darkArtsVariables.lineRenderer.SetPosition(2, savedDarkArtsEnemies[localCount +2 ].transform.position);

//void AddToDarkArtsRenderer(Transform transform)
//{
//darkArtsVariables.lineRenderer.positionCount++;
//darkArtsVariables.lineRenderer.SetPosition(darkArtsVariables.lineRenderer.positionCount -1, transform.position);
//darkArtsPositionPoints.Capacity++;
//darkArtsPositionPoints[^1] = transform.position;
//darkArtsPositionPoints.Add(transform.position);

//}

//darkArtsPositionPoints = darkArtsPositionPoints.Skip(0).ToArray();
            
//darkArtsVariables.lineRenderer.SetPosition(0, darkArtsPositionPoints[0]);
//darkArtsVariables.lineRenderer.SetPosition(1, darkArtsPositionPoints[1]);
//darkArtsVariables.lineRenderer.SetPosition(2, darkArtsPositionPoints[2]);

/*if(savedDarkArtsEnemies.Capacity > 0) foreach (var enemy in savedDarkArtsEnemies)
        {
            enemy.Knockbacked = false;
            enemy.TakeDamage(playerStats.Damage * darkArtsVariables.damageMultiplier, enemy.transform.position);
            yield return new WaitForSeconds(0.03f);
        }*/
        
        
//Vector3[] darkArtsPositionPoints = new Vector3[savedDarkArtsEnemies.Capacity - 1];
//new Vector3[darkArtsVariables.lineRenderer.positionCount];
