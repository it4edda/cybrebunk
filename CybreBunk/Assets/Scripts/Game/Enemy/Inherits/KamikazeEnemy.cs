using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class KamikazeEnemy : EnemyBehaviour
{
    [Header("Uni-bomber Exclusive")]
    [SerializeField] GameObject explosion;
    [SerializeField] float suicideRange;
    void Update()
    {
        if (Vector2.Distance(target.position, transform.position) < suicideRange && !IsStunned) Die();
    }
    protected override void Die()
    {
        Instantiate(explosion, transform.position, quaternion.identity);
        base.Die();
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, suicideRange);
    }
        
    
}
