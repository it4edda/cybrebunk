using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Ability : MonoBehaviour
{
    [Header("Here's your header Alex")]
    public ChosenAbility ability1;
    PlayerMovement playerMovement;
    PlayerStats    playerStats;
    PlayerCamera   cam;
    AudioSource    audioSource;
    float cooldown;

    public float Cooldown1 => cooldown;
    bool canActivateAbilities = true;
    
    [Header("Dark Arts"), SerializeField] public DarkArtsVariables darkArtsVariables;
    List<EnemyBehaviour>                                    savedDarkArtsEnemies = new List<EnemyBehaviour>();
    //List<Vector3>                                           darkArtsPositionPoints;
    
    [Header("AoeAttack"), SerializeField] public AoeAttackVariables aoeAttackVariables;
    [Header("Blast"), SerializeField]     public BlastVariables     blastVariables;

    public enum ChosenAbility
    {
        None,
        DarkArts,
        AoeAttack,
        Blast
    }

    void Start()
    {
        ability1                                       = ChosenAbility.None;
        audioSource                                    = GetComponent<AudioSource>();
        playerStats                                    = FindObjectOfType<PlayerStats>();
        playerMovement                                 = GetComponent<PlayerMovement>();
        cam                                            = FindObjectOfType<PlayerCamera>();
        darkArtsVariables.baseVariables.canUseAbility  = true;
        aoeAttackVariables.baseVariables.canUseAbility = true;
        darkArtsVariables.activeParticles.Stop();
    }

    void Update()
    {
        Cooldown();
    }
    void Cooldown()
    {
        if (!(cooldown > 0)) return;
        cooldown -= Time.deltaTime;
        if (cooldown <= 0)
        {
            canActivateAbilities = true;
        }
    }

    public void InitiateAbility(ChosenAbility chosen)
    {
        switch (chosen)
        {
            case ChosenAbility.None:
                Debug.Log("haha fuckign retard");
                break;
            
            case ChosenAbility.DarkArts:
                if (darkArtsVariables.baseVariables.canUseAbility && canActivateAbilities)
                {
                    StartCoroutine(DarkArts());
                    canActivateAbilities = false;
                }

                break;

            case ChosenAbility.AoeAttack:
                if (aoeAttackVariables.baseVariables.canUseAbility && canActivateAbilities)
                {
                    StartCoroutine(AoeAttack());
                    canActivateAbilities = false;
                }

                break;

            case ChosenAbility.Blast:
                if (blastVariables.baseVariables.canUseAbility && canActivateAbilities)
                {
                    BlastAttack();
                    canActivateAbilities = false;
                }
                
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
                if (!other.GetComponent<EnemyBehaviour>().canDarkArts) return;
                savedDarkArtsEnemies.Add(other.GetComponent<EnemyBehaviour>());
                savedDarkArtsEnemies[^1].IsStunned = true;
            }
        }
    }

#region Vinushka
#region  Variables
    [Serializable]
    public struct BaseVariables
    {
        public bool  canUseAbility;
        public float abilityCooldown;
    }
    
    [Serializable] public struct DarkArtsVariables
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

        public AudioClip slashSound;
        public AudioClip sheatheSound;
            
        public void ToggleAbility(bool enable)
        {
            baseVariables.canUseAbility = !enable;
            isActive                    = enable;
            playerSprite.color          = enable ? playerColorWhileActive : playerColorAtStart;
            if (enable) activeParticles.Play(); else activeParticles.Stop();
        }
    }
    [Serializable] public struct AoeAttackVariables
    {
        public BaseVariables baseVariables;

        public AudioClip  guitarSound;
        public float      timeActive;
        public Animator   slasher;
        public GameObject colliderObject;
    }
    
    [Serializable] public struct BlastVariables
    {
        public BaseVariables baseVariables;

        public CustomBulletShooter shooter;
        public CustomBulletPattern pattern;

        public AudioClip blastSound;
    }
#endregion
    IEnumerator DarkArts() //name of the item in isaac, im not THAT edgy
    {
        darkArtsVariables.ToggleAbility(true);
        cam.CameraShake(0.1f, 0.2f);
        playerStats.GodMode(true);
        playerMovement.MoveSpeed += Vector2.one * darkArtsVariables.movementBoost;
        //become gray

        yield return new WaitForSeconds(darkArtsVariables.timeActive);
        
        playerMovement.MoveSpeed -= Vector2.one * darkArtsVariables.movementBoost;

        yield return new WaitForSeconds(1);
        //GWYN: LORD OF CINDER          (aldin snap)
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
        
        foreach (var enemy in savedDarkArtsEnemies)
        {
            darkArtsVariables.lineRenderer.SetPosition(0, enemyPositions[localCount]);
            darkArtsVariables.lineRenderer.SetPosition(1, enemyPositions[Math.Max(localCount - 1, 0)]);
            darkArtsVariables.lineRenderer.SetPosition(2, enemyPositions[Math.Max(localCount - 2, 0)]);
            
            yield return new WaitForSeconds(0.05f);

            if (enemy != null)
            {
                audioSource.PlayOneShot(darkArtsVariables.slashSound);
                cam.CameraShake(0.01f, 0.2f);
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
        cooldown = darkArtsVariables.baseVariables.abilityCooldown;
        darkArtsVariables.ToggleAbility(false);
        playerStats.GodMode(false);
    }
    
    IEnumerator AoeAttack()
    {
        cam.CameraShake(0.4f);
        aoeAttackVariables.baseVariables.canUseAbility = false;
        aoeAttackVariables.slasher.gameObject.SetActive(true);
        audioSource.PlayOneShot(aoeAttackVariables.guitarSound);
        //Vector3 a = weaponGraphics.localScale;
        //weaponGraphics.localScale = new Vector3(a.x, a.y * -1, a.z);
        yield return new WaitForSeconds(aoeAttackVariables.slasher.GetCurrentAnimatorStateInfo(0).length);
        aoeAttackVariables.colliderObject.SetActive(true);
        aoeAttackVariables.slasher.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        cooldown = aoeAttackVariables.baseVariables.abilityCooldown;
        aoeAttackVariables.colliderObject.SetActive(false);
        aoeAttackVariables.baseVariables.canUseAbility = true;

    }

    #endregion
#region Blast

    void BlastAttack()
    {
        cooldown = blastVariables.baseVariables.abilityCooldown;
        audioSource.PlayOneShot(blastVariables.blastSound);
        
        blastVariables.shooter.ChooseNewRoutine();
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
