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
    
    [Header("Other")]
    [SerializeField] float movementSpeed;
    [SerializeField] float knockbackStrength = 5f;
    [SerializeField] int   enemyGaugePrice;
    [SerializeField] float attackRange;
    
    protected bool      midAttack;
    bool                isStunned;
    
    protected Transform target;
    Transform           bloodParent;
    Rigidbody2D         rb;
    EnemySpawning       enemySpawning;
    UserInterfaceGauge  gauge;

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
    void OnTriggerEnter(Collider other)
    {
        //todo contact damage
        if (other.CompareTag("Player"))
        {
            //add a trigger collider 
            //deal damage
            //other.GetComponent<PlayerStats>()
        }
    }
    void FixedUpdate()
    {
        if (!target || isStunned)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        Movement();

        if (InRange() && !midAttack)
        Attack();
    }
    public void TakeDamage(int damage, Vector2 dir)
    {
        //do knockback
        StartCoroutine(Knockback(dir));
        health -= damage;
        if (health <= 0) Die();
        //health |= (health <= 0);
    }
    IEnumerator Knockback(Vector2 dir)
    {
        isStunned = true;
        Vector2 direction =  (Vector2)transform.position - dir;
        rb.AddForce(direction * knockbackStrength, ForceMode2D.Impulse);
        yield return new WaitForSeconds(1);
        isStunned = false;
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
