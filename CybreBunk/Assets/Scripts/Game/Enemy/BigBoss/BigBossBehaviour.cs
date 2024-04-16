using System.Collections;
using UnityEngine;

public class BigBossBehaviour : EnemyBehaviour
{
    [SerializeField] float                   timeBetweenSlams;
    [SerializeField] MegaEyes[]              eyes;
    [SerializeField] HandSlam[]              hands;
    UserInterfaceBossHealth healthBar;
    bool                                     rightHand;
    protected override void Start()
    {
        base.Start();
        healthBar = FindObjectOfType<UserInterfaceBossHealth>();
        healthBar.transform.GetChild(0).gameObject.SetActive(true);
        healthBar.SetValues(health, 0);
        
    }
    public override void TakeDamage(int damage, Vector2 dir)
    {
        base.TakeDamage(damage, dir);
        healthBar.UpdateHealthValue(health);
    }
    protected override void Die()
    {
        GetComponent<Collider2D>().gameObject.SetActive(false);
        //base.Die();
        enemySpawning.CanSpawn = true;
        enemySpawning.StartSpawning();
        FindObjectOfType<PlayerCamera>().SetFollow();
        FindObjectOfType<SatanicC>().BossDeath();
        healthBar.transform.GetChild(0).gameObject.SetActive(false);
        Destroy(gameObject);
    }
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
        yield return null;
        
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
