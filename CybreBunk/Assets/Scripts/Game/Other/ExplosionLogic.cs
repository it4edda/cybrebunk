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
        //TODO Something is fucking up with this, stand still in melee and it works well, otherwise its sheit
        bool         playerHit = false;
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, range);

        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player") && !playerHit)
            {
                //TODO Debug.Log("haha times hit player");
                playerHit = true;
                var playerStats = hitCollider.GetComponent<PlayerStats>();
                if (playerStats != null)
                {
                    playerStats.Health = -damage;
                }
            }
            else if (hitCollider.CompareTag("Enemy"))
            {
                hitCollider.GetComponent<EnemyBehaviour>()?.TakeDamage(1, transform.position);
            }
        }

        yield return new WaitForSeconds(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
