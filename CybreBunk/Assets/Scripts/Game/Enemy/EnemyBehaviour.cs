using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("Mortality fields")] 
    public bool canDarkArts = true;
    [SerializeField] protected int health;
    [SerializeField] bool         isBig;
    [SerializeField] GameObject[] smallBlood;
    [SerializeField] GameObject[] bigBlood;
    [SerializeField] ParticleSystem    bloodParticle;

    [Header("Other")]
    [SerializeField] protected float movementSpeed;
    [SerializeField] float knockbackStrength = 5f;
    [SerializeField] int   enemyGaugePrice;
    [SerializeField] protected float attackRange;

    public int belongsToWaveNumber;
    
    protected AudioSource audioSource;
    protected bool        midAttack;
    bool                  knockbacked;
    bool                  isStunned;
    
    protected Transform     target;
    PlayerStats             playerStats;
    Transform               bloodParent;
    protected Rigidbody2D   rb;
    protected EnemySpawning enemySpawning;
    UserInterfaceGauge      gauge;
    Animator                animator;

    public bool Knockbacked
    {
        get => knockbacked;
        set => knockbacked = value;
    }
    public bool IsStunned
    {
        get => isStunned;
        set => isStunned = value;
    }
    protected virtual void Start()
    {
        audioSource   = GetComponent<AudioSource>();
        enemySpawning = FindObjectOfType<EnemySpawning>();
        bloodParent   = FindObjectOfType<SatanicC>().transform.Find("BloodParent");
        rb            = GetComponent<Rigidbody2D>();
        animator      = GetComponentInChildren<Animator>();
        target        = FindObjectOfType<PlayerMovement>().transform;
        gauge         = FindObjectOfType<UserInterfaceGauge>();
        playerStats = FindObjectOfType<PlayerStats>();
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerStats.Health = -1;
            StartCoroutine(Knockback(other.transform.position));
        }
    }
    void FixedUpdate()
    {
        if (isStunned)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        if (!target || Knockbacked) return;
        
        Movement();

        if (InRange()) Attack();
    }
    public virtual void TakeDamage(int damage, Vector2 dir)
    {
        Vector2 bloodDir =playerStats.transform.position - transform.position;
        ParticleSystem particleSystemObject = Instantiate(bloodParticle, transform.position, Quaternion.identity);

        Quaternion rotation = Quaternion.LookRotation(bloodDir, Vector3.forward);

        particleSystemObject.transform.rotation = rotation;
        particleSystemObject.transform.Rotate(0f, 180f, 0f);
        particleSystemObject.Play();
        
        
        if(!isBig) StartCoroutine(Knockback(dir));
        health -= damage;
        if (health <= 0) Die();
    }
    protected virtual IEnumerator Knockback(Vector2 dir)
    {
        Knockbacked = true;
        Vector2 direction =  (Vector2)transform.position - dir;
        direction.Normalize();
        direction /= 5;
        
        
        rb.AddForce(direction * knockbackStrength, ForceMode2D.Impulse);
        yield return new WaitForSeconds(1);
        Knockbacked = false;
    }
    protected virtual void Die()
    {
        var a = Instantiate(isBig ? bigBlood[Random.Range(0, bigBlood.Length)] : smallBlood[Random.Range(0, smallBlood.Length)] , transform.position, quaternion.identity );
        a.transform.parent = bloodParent;
        enemySpawning.DecreaseEnemyAliveNumber(belongsToWaveNumber, transform.position);
        gauge.UpdateGaugeSlider(enemyGaugePrice + playerStats.IncreaseInBloodGain);
        Destroy(gameObject);
    }
    protected virtual void Movement()
    {
        Vector3 movement = Vector3.Normalize(target.position - transform.position);
        if (animator) animator.transform.localScale = new Vector3(movement.x > 0 ? -1 : 1, 1, 1);
        //transform.position += movement * (movementSpeed * Time.deltaTime);
        // rb.AddForce(movement * movementSpeed);
        rb.velocity = movement * (movementSpeed * Time.deltaTime);
    }
    protected virtual void Attack() 
    {
        //FILL THIS WITH SOMETHING
        Debug.Log(gameObject.name + " is attacking!");
    }
    protected bool InRange() => Vector2.Distance(target.position, transform.position) < attackRange;
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
