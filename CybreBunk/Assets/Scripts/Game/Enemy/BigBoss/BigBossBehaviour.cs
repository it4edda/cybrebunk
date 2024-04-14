using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BigBossBehaviour : EnemyBehaviour
{
    protected override IEnumerator Knockback(Vector2 dir) { yield return null;}
}
