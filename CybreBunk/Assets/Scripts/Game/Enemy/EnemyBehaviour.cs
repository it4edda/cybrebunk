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
    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerMovement>().transform;
        //todo find player and move towards him ( basic movement for father script)
        //todo be able to take damage and die
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        Movement();
    }
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
            Die();
    }
    void Die()
    {
        Instantiate(smallBlood[Random.Range(0, smallBlood.Length)], transform.position, quaternion.identity );
        Destroy(gameObject);
    }
    protected virtual void Movement()
    {
        Vector3 movement = Vector3.Normalize(target.position - transform.position);
        transform.position += movement * (movementSpeed * Time.deltaTime);
    }
}
