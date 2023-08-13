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
    [SerializeField] bool         isBig = false;
    [SerializeField] GameObject[] smallBlood;
    [SerializeField] GameObject[] bigBlood;
    [SerializeField] float        movementSpeed;
    Transform                     target;
    bool                          isStunned = false;
    void Start()
    {
        target = FindObjectOfType<PlayerMovement>().transform;
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
        if (!target) return;
        Movement();
    }
    public void TakeDamage(int damage)
    {
        //do knockback
        health -= damage;

        if (health <= 0)
            Die();
    }
    IEnumerator Knockback()
    {
        isStunned = true;
        yield return new WaitForSeconds(1);
        isStunned = false;
    }
    protected virtual void Die()
    {
        Instantiate(isBig ? bigBlood[Random.Range(0, bigBlood.Length)] : smallBlood[Random.Range(0, smallBlood.Length)] , transform.position, quaternion.identity );
        Destroy(gameObject);
    }
    protected virtual void Movement()
    {
        Vector3 movement = Vector3.Normalize(target.position - transform.position);
        transform.position += movement * (movementSpeed * Time.deltaTime);
    }
}
