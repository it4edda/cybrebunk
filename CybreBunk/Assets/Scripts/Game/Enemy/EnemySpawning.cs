using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawning : MonoBehaviour
{
    [SerializeField] float      timer;
    [SerializeField] float      spawnRadius;
    [SerializeField] GameObject tempEnemySingular;

    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(timer);

        // Calculate a random angle in radians
        float randomAngle = Random.Range(0f, 2f * Mathf.PI);

        // Calculate random position outside the spawn radius
        Vector3 spawnPosition = transform.position + new Vector3(
                                                                 Mathf.Cos(randomAngle) * spawnRadius,
                                                                 Mathf.Sin(randomAngle) * spawnRadius,
                                                                 0f
                                                                );

        // Spawn enemy at the calculated position
        Instantiate(tempEnemySingular, spawnPosition, Quaternion.identity);

        StartCoroutine(Spawn());
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
