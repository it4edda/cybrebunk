using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChimeraBoss : EnemyBehaviour
{
    #region Variables
    
    [SerializeField] Transform moveTarget;
    [Header("Shooter tail")]
    [SerializeField] CustomBulletShooter snakeShooter;
    [SerializeField] float rotationSpeed;
    [SerializeField] float rotationMod;
    [Header("Charge")]
    [SerializeField] int numberOfChargesPerCycle;
    [SerializeField] float chargeIndicatorTime;
    [SerializeField] float timePerCharge;
    [SerializeField] float chargeForce;
    [SerializeField] float timeBetweenCharges;
    [SerializeField] float timeBetweenCycles;

    UserInterfaceBossHealth healthBar;
    bool                    isBigCharging;

    #endregion

    #region SetUp

    protected override void Start()
    {
        base.Start();
        healthBar = FindObjectOfType<UserInterfaceBossHealth>();
        healthBar.transform.GetChild(0).gameObject.SetActive(true);
        healthBar.SetValues(health, 0);
        
        moveTarget = FindNearestMovementTarget();
        StartCoroutine(BigCharge());
    }
    #endregion
    

    #region TheBigCharge
    IEnumerator BigCharge()
    {
        snakeShooter.canAttack = false;
        for (int i = 0; i < numberOfChargesPerCycle; i++)
        { 
            isBigCharging = true;
            rb.velocity = Vector2.zero;
            Vector2 chargeDir = Vector3.Normalize(target.position - transform.position);
            yield return new WaitForSeconds(chargeIndicatorTime);
            
            rb.AddForce(chargeDir * chargeForce);
            yield return new WaitForSeconds(timePerCharge);
            
            rb.velocity = Vector2.zero;
            isBigCharging = false;
            moveTarget = FindNearestMovementTarget();
            yield return new WaitForSeconds(timeBetweenCharges);
        }

        snakeShooter.canAttack = true;
        snakeShooter.ChooseNewRoutine();

        yield return new WaitForSeconds(timeBetweenCycles);
        StartCoroutine(BigCharge());
    }
    #endregion

    #region Movement

    //charging and stand in center
    protected override void Movement()
    {
        RotateSnakeTail();
        if (isBigCharging) { return;}
        Vector3 movement = Vector3.Normalize(moveTarget.position - transform.position);
        
        //transform.position += movement * (movementSpeed * Time.deltaTime);
        // rb.AddForce(movement * movementSpeed);
        rb.velocity = movement * (movementSpeed * Time.deltaTime);
    }
    public override void TakeDamage(int damage, Vector2 dir)
    {
        base.TakeDamage(damage, dir);
        healthBar.UpdateHealthValue(health);
    }
    void RotateSnakeTail()
    {
        Vector3 v2Target = target.position - transform.position;
        float ang = Mathf.Atan2(v2Target.y, v2Target.x) * Mathf.Rad2Deg * rotationMod;
        Quaternion q = Quaternion.AngleAxis(ang, Vector3.forward);
        snakeShooter.firePos.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotationSpeed);
    }
    Transform FindNearestMovementTarget()
    {
        Transform nearestTarget = null;

        List<GameObject> availableTargets = new List<GameObject>(GameObject.FindGameObjectsWithTag("ChimeraTargets"));
        
        foreach (GameObject target in availableTargets)
        {
            if (nearestTarget == null)
            {
                nearestTarget = target.transform;
            }else if (Vector3.Distance(transform.position, target.transform.position) < Vector3.Distance(transform.position, nearestTarget.position))
            {
                nearestTarget = target.transform;
            }
        }

        return nearestTarget;
    }
    protected override IEnumerator Knockback(Vector2 dir) { yield return null;}
    #endregion

    protected override void Die()
    {
        enemySpawning.CanSpawn = true;
        enemySpawning.StartSpawning();
        healthBar.transform.GetChild(0).gameObject.SetActive(false);
        FindObjectOfType<PlayerCamera>().SetFollow();
        Destroy(gameObject);
    }
}
