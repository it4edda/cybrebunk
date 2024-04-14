using UnityEngine;

public class ShootingEnemy : EnemyBehaviour
{
    //make the enemy shoot toward the player, bullets shall ignore fellow enemies
    [Header("Shooter exclusive")]
    [SerializeField] CustomBulletShooter shooter;
    [SerializeField] float rotationMod;
    [SerializeField] float rotationSpeed;

    protected override void Attack()
    {
        if (shooter.isAttacking) { return; }
        shooter.ChooseNewRoutine();
    }

    protected override void Movement()
    {
        LookAtPlayer();
        if (Vector2.Distance(target.position, transform.position) <= (attackRange*0.60f)) { rb.velocity = Vector2.zero; return; }
        base.Movement();
    }

    void LookAtPlayer()
    {
        Vector3 v2Target = target.position - transform.position;
        float   ang      = Mathf.Atan2(v2Target.y, v2Target.x) * Mathf.Rad2Deg * rotationMod;
        Quaternion q = Quaternion.AngleAxis(ang, Vector3.forward);
        shooter.firePos.rotation = Quaternion.Slerp(transform.rotation, q ,Time.deltaTime * rotationSpeed);
    }
}
