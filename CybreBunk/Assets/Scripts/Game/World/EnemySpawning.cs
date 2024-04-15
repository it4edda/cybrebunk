using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering.UI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
public class EnemySpawning : MonoBehaviour
{
    [SerializeField] EnemyWave[] beginnerWaves;
    [SerializeField] EnemyWave[] preFirstBossRandom;
    [SerializeField] EnemyWave[] preSecondBossRandom;
    [SerializeField] EnemyWave[] preThirdBossRandom;
    [SerializeField] GameObject  itemPrefab;
    [SerializeField] float       timer;
    [SerializeField] float       spawnRadius;
    [SerializeField] Transform   player;

    int                  waveNumber         = 0;
    Dictionary<int, int> enemiesAliveByWave = new Dictionary<int, int>();
    bool                 firstWaveSpawned   = false;

    void Start() { StartCoroutine(Spawn()); }

    void Update()
    {
        if (firstWaveSpawned && IsAllWavesCleared()) { StartCoroutine(Spawn()); }
    }

    IEnumerator Spawn()
    {
        while (waveNumber < beginnerWaves.Length)
        {
            float   randomAngle   = Random.Range(0f, 2f * Mathf.PI);
            Vector3 spawnPosition = player.position + new Vector3(Mathf.Cos(randomAngle) * spawnRadius, Mathf.Sin(randomAngle) * spawnRadius, 0f);

            
            if (!enemiesAliveByWave.ContainsKey(waveNumber))  enemiesAliveByWave[waveNumber] = 0; 

            foreach (var enemyPrefab in beginnerWaves[waveNumber].contestants)
            {
                Vector3 enemySpawnPosition = spawnPosition + new Vector3(RandomValue(), RandomValue(), RandomValue());
                var a = Instantiate(enemyPrefab, enemySpawnPosition, Quaternion.identity);
                a.GetComponent<EnemyBehaviour>().belongsToWaveNumber = waveNumber;
                enemiesAliveByWave[waveNumber]++; 
            }

            
            waveNumber++;
            yield return new WaitForSeconds(timer);
        }
    }



public void DecreaseEnemyAliveNumber(int waveNumber, Vector3 enemyPosition)
{
    if (enemiesAliveByWave.ContainsKey(waveNumber))
    {
        enemiesAliveByWave[waveNumber]--;
        if (enemiesAliveByWave[waveNumber] <= 0)
        {
            if (beginnerWaves[waveNumber].spawnItem) 
                Instantiate(itemPrefab, enemyPosition, Quaternion.identity);
            
            enemiesAliveByWave.Remove(waveNumber); 
        }
    }
}

    bool IsAllWavesCleared()
    {
        foreach (var kvp in enemiesAliveByWave)
        {
            if (kvp.Value > 0) return false;
        }

        return true;
    }

    void OnDrawGizmosSelected() { Gizmos.DrawWireSphere(player.position, spawnRadius); }

    float RandomValue() => Random.Range(-3, 3) * 0.05f;

    [Serializable]
    struct EnemyWave
    {
        public GameObject[] contestants;
        public bool         spawnItem;
    }
}

