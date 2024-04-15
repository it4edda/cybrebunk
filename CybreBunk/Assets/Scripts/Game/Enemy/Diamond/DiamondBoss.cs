using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class DiamondBoss : EnemyBehaviour
{
    [SerializeField] CustomBulletShooter shooter;
    [SerializeField] GameObject[]        enemiesToSpawn;
    [SerializeField] Vector3[]           spawnPositions;
    [SerializeField] float               breakTime;
    [SerializeField] float               timeBetweenShove;
    [SerializeField] int                 timesToShove;
    [SerializeField] AudioClip           enemyBoop;
    //AudioSource                          audioSource;
    int                                  shoving;
    bool                                 shooting;
    
    protected override void Start()
    {
        base.Start();
        StartCoroutine(SpawnWaves());
        shoving = timesToShove;
    }
    
    IEnumerator SpawnWaves()
    {
        shooting = false;

        foreach (var i in enemiesToSpawn)
        {
            audioSource.PlayOneShot(enemyBoop);
            Instantiate(i, transform.position + spawnPositions[Random.Range(0, spawnPositions.Length)] ,Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
        }
        
        yield return new WaitForSeconds(timeBetweenShove);

        shoving--;

        if (shoving <= 0)
        {
            shoving = timesToShove;
            shooter.ChooseNewRoutine();
        }
        yield return new WaitForSeconds(breakTime);
        StartCoroutine(SpawnWaves());
    }
    protected override IEnumerator Knockback(Vector2 dir) { yield break; }
    protected override void Movement() { return; }

    void OnDrawGizmosSelected()
    {
        foreach (var i in spawnPositions)
        {
            Gizmos.DrawWireSphere(i+ transform.position, 0.02f);
        }
    }
}
