using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] bool isAllied;
    [SerializeField] bool oneHitLife = false;
    [SerializeField] float damageMultiplier;
    int damage;
    public virtual void Start() { damage = Mathf.RoundToInt(FindObjectOfType<PlayerStats>().Damage * damageMultiplier); }
    void OnTriggerEnter2D(Collider2D other)
    {
        switch (isAllied)
        {
            case true when other.CompareTag("Enemy"):
                other.GetComponent<EnemyBehaviour>().TakeDamage(damage);
                if (oneHitLife) Destroy(gameObject);
                break;

            case false when other.CompareTag("Player"):
                other.GetComponent<PlayerStats>().Health--;
                if (oneHitLife) Destroy(gameObject);
                break;
        }
    }
}
