using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionLogic : MonoBehaviour
{
    [SerializeField] float range  = 3f;
    [SerializeField] int   damage = 3;
    void Start() => StartCoroutine(Deed());
    IEnumerator Deed()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, range);

        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                var playerStats = hitCollider.GetComponent<PlayerStats>();
                if (playerStats != null)
                {
                    playerStats.Health = -damage;
                    Debug.Log("Hit player");
                }
            }
            else if (hitCollider.CompareTag("Enemy"))
            {
                // Deal damage to enemy
            }
        }

        yield return new WaitForSeconds(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }
}
