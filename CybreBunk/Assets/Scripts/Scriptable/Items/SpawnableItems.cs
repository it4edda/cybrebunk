using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "Items/SpawningItem")]
public class SpawnableItems : ItemData
{
    public SpawnableObjects spawnableObjects;

    [Serializable]
    public struct SpawnableObjects
    {
        public bool spawnRepeatedly;
        public float timeBetweenSpawns;
        public GameObject objectToSpawn;
        public SpawnPos spawnPos;
        public float minRadius;
        public float maxRadius;
    }

    [Serializable]
    public enum SpawnPos
    {
        OnPlayer,
        Radius
    }
    
    public override void OnPickup(PlayerStats playerStats)
    {
        FindObjectOfType<PlayerInventory>().StartSpawning(playerStats, spawnableObjects);
    }
}
