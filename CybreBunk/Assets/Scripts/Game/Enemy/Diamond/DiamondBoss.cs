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
    UserInterfaceBossHealth              healthBar;
    int                                  shoving;
    bool                                 shooting;
    
    protected override void Start()
    {
        base.Start();
        healthBar = FindObjectOfType<UserInterfaceBossHealth>();
        healthBar.transform.GetChild(0).gameObject.SetActive(true);
        healthBar.SetValues(health, 0);
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
    protected override void Die()
    {
        //base.Die();
        var a = FindObjectOfType<SatanicC>();
        a.CallBloodGround();
        a.canConsume           = true;
        enemySpawning.CanSpawn = true;
        enemySpawning.StartSpawning();
        healthBar.transform.GetChild(0).gameObject.SetActive(false);
        FindObjectOfType<PlayerCamera>().SetFollow();
        GetComponentInChildren<Animator>().SetTrigger("Die");
    }
    public override void TakeDamage(int damage, Vector2 dir)
    {
        base.TakeDamage(damage, dir);
        healthBar.UpdateHealthValue(health);
    }
    protected override IEnumerator Knockback(Vector2 dir) { yield break; }
    protected override void        Movement()             { return; }

    void OnDrawGizmosSelected()
    {
        foreach (var i in spawnPositions)
        {
            Gizmos.DrawWireSphere(i+ transform.position, 0.02f);
        }
    }
}
