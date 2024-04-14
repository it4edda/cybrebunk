using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BigBossBehaviour : EnemyBehaviour
{
    [SerializeField] float      timeBetweenSlams;
    [SerializeField] MegaEyes[] eyes;
    [SerializeField] HandSlam[] hands;
    bool                        rightHand;
    IEnumerator HandSlams()
    {
        if (rightHand) hands[0].StartSlam();
        else hands[1].StartSlam();
        yield return new WaitForSeconds(timeBetweenSlams);
        rightHand = !rightHand;
        StartCoroutine(HandSlams());
    }
    IEnumerator IntroHandSlams()
    {
        hands[0].FireSlamBullets();
        hands[1].FireSlamBullets();
        yield return new WaitForSeconds(timeBetweenSlams);
        eyes[0].ShootEyes();
        eyes[1].ShootEyes();
        StartCoroutine(HandSlams());
    }
    

    protected override void Movement() { return; }
    protected override IEnumerator Knockback(Vector2 dir) { yield return null;}
}
