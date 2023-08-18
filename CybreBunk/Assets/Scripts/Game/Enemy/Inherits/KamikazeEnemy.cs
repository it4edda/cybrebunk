using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class KamikazeEnemy : EnemyBehaviour
{
    [Header("Uni-bomber Exclusive")]
    [SerializeField] GameObject explosion;
    protected override void Die()
    {
        Instantiate(explosion, transform.position, quaternion.identity); //give this random rotation?
        base.Die();
    }
}
