using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] bool isAllied;
    [SerializeField] float damageMultiplier;
    int damage;
    void Start() { damage = Mathf.RoundToInt(FindObjectOfType<PlayerStats>().Damage * damageMultiplier); }
    void OnTriggerEnter2D(Collider2D other)
    {
        switch (isAllied)
        {
            case true when other.CompareTag("Enemy"):
                other.GetComponent<EnemyBehaviour>().TakeDamage(damage);
                break;

            case false when other.CompareTag("Player"):
                other.GetComponent<PlayerStats>().Health--;
                break;
        }
    }
}
