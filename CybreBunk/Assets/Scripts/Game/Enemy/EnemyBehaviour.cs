using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("Mortality fields")]
    [SerializeField] int health;
    [SerializeField] bool         isBig;
    [SerializeField] GameObject[] smallBlood;
    [SerializeField] GameObject[] bigBlood;
    [SerializeField] ParticleSystem    bloodParticle;
    
    [Header("Other")]
    [SerializeField] float movementSpeed;
    [SerializeField] float knockbackStrength = 5f;
    [SerializeField] int   enemyGaugePrice;
    [SerializeField] float attackRange;
    
    protected bool midAttack;
    bool           knockbacked;
    bool           isStunned;
    
    protected Transform target;
    Transform           bloodParent;
    Rigidbody2D         rb;
    EnemySpawning       enemySpawning;
    UserInterfaceGauge  gauge;

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
        enemySpawning = FindObjectOfType<EnemySpawning>();
        bloodParent   = FindObjectOfType<SatanicC>().transform.Find("BloodParent");
        rb            = GetComponent<Rigidbody2D>();
        target        = FindObjectOfType<PlayerMovement>().transform;
        gauge         = FindObjectOfType<UserInterfaceGauge>();
    }
    void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("fix this damage on contact");
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStats>().Health = -1;
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

        if (InRange() && !midAttack) Attack();
    }
    public void TakeDamage(int damage, Vector2 dir)
    {
        StartCoroutine(Knockback(dir));
        health -= damage;
        if (health <= 0) Die();
    }
    IEnumerator Knockback(Vector2 dir)
    {
        Knockbacked = true;
        Vector2 direction =  (Vector2)transform.position - dir;

        //SOME BLOOD SHIT
        // var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        // bloodParticle.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        // bloodParticle.Play();
        
        rb.AddForce(direction * knockbackStrength, ForceMode2D.Impulse);
        yield return new WaitForSeconds(1);
        Knockbacked = false;
    }
    protected virtual void Die()
    {
        var a = Instantiate(isBig ? bigBlood[Random.Range(0, bigBlood.Length)] : smallBlood[Random.Range(0, smallBlood.Length)] , transform.position, quaternion.identity );
        a.transform.parent = bloodParent;
        enemySpawning.DecreaseEnemyAliveNumber();
        gauge.UpdateGaugeSlider(enemyGaugePrice);
        Destroy(gameObject);
    }
    protected virtual void Movement()
    {
        Vector3 movement = Vector3.Normalize(target.position - transform.position);
        //transform.position += movement * (movementSpeed * Time.deltaTime);
        // rb.AddForce(movement * movementSpeed);
        rb.velocity = movement * (movementSpeed * Time.deltaTime);
    }
    protected virtual void Attack() 
    {
        //FILL THIS WITH SOMETHING
        Debug.Log(gameObject.name + " is attacking!");
    }
    protected bool InRange()              => Vector2.Distance(target.position, transform.position) < attackRange;
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
