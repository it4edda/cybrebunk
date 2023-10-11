using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] bool  isAllied;
    [SerializeField] bool  oneHitLife = false;
    [SerializeField] float damageMultiplier;
    int                    damage;
    public static event Action<GameObject> OnHitEvent; //make a static class possibly
    protected virtual void Start()
    {
        damage = Mathf.RoundToInt(FindObjectOfType<PlayerStats>().Damage); //* damageMultiplier);
        damage = damage <= 0 ? 1 : Mathf.Abs(damage);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        switch (isAllied)
        {
            case true when other.CompareTag("Enemy"):
                other.GetComponent<EnemyBehaviour>().TakeDamage(damage, transform.position);
                OnHitEvent?.Invoke(other.gameObject);
                if (oneHitLife) Destroy(gameObject);
                break;

            case false when other.CompareTag("Player"):
                other.GetComponent<PlayerStats>().Health--;
                if (oneHitLife) Destroy(gameObject);
                break;
        }
    }
    static void OnOnHitEvent(GameObject obj)
    {
        OnHitEvent?.Invoke(obj); 
    }
}
