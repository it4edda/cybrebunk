using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingBossBehaviour : EnemyBehaviour
{
    [SerializeField] GameObject bombPrefab;
    [SerializeField] LineRenderer lineRenderer;
    protected override void Attack() //DASH AND DROP BOMBS
    {
        base.Attack();
    }

    protected override IEnumerator Knockback(Vector2 dir) { yield break; }
}