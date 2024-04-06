using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawning : MonoBehaviour
{
    [SerializeField] EnemyWave[] waves;
    [SerializeField] float       timer;
    [SerializeField] float       spawnRadius;
    [SerializeField] Transform   player;
    int                          waveNumber = 0;
    int                          enemiesAlive;
    bool                         firstWaveSpawned = false;
    void Start()
    {
        StartCoroutine(Spawn());
    }
    void Update()
    {
        if (enemiesAlive <= 0 && firstWaveSpawned) StartCoroutine(Spawn());
    }
    IEnumerator Spawn()
    {
        if (waveNumber >= waves.Length) yield break;
        float randomAngle = Random.Range(0f, 2f * Mathf.PI);

        Vector3 spawnPosition = player.position + new Vector3(Mathf.Cos(randomAngle) * spawnRadius, 
                                                                 Mathf.Sin(randomAngle) * spawnRadius, 0f);
        
        foreach (var i in waves[waveNumber].Contestants)
        {
            Instantiate(i, spawnPosition + new Vector3(RandomValue(),RandomValue(),RandomValue()), quaternion.identity);
        }
        enemiesAlive += waves[waveNumber].Contestants.Length;
        waveNumber++;
        yield return new WaitForSeconds(timer);
        firstWaveSpawned = true;
        //if all enemies are dead => return;
        if (enemiesAlive > 0) StartCoroutine(Spawn());
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(player.position, spawnRadius);
    }
    
    //add motherwaves, spawn in waves of enemies in waves, (first 5, then 6, then 10)
    //add waves, if wave is killed => spawn new wave. If player takes too long, spawn in next wave.
    public void DecreaseEnemyAliveNumber() => enemiesAlive--;
    float  RandomValue()              => Random.Range(-3, 3) * 0.05f;
    [Serializable] struct EnemyWave { public GameObject[] Contestants; }
}

