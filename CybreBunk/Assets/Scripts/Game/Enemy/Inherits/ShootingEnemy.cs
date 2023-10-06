using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ShootingEnemy : EnemyBehaviour
{
    //make the enemy shoot toward the player, bullets shall ignore fellow enemies
    [Header("Shooter exclusive")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float  attackSpeed;

    protected override void Attack() => StartCoroutine(Shoot()); 
    IEnumerator Shoot()
    {
        midAttack = true;
        Instantiate(bulletPrefab, transform.position, quaternion.LookRotation(target.position, transform.up));
        yield return new WaitForSeconds(attackSpeed);
        midAttack = false;
    }
    protected override void Movement()
    {
        //base.Movement();
        
    }
}
