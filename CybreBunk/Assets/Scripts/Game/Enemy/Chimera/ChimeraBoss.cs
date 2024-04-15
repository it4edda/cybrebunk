using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChimeraBoss : EnemyBehaviour
{
    #region Variables

    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform moveTarget;
    [SerializeField] float movementSpeed;
    [Header("Shooter tail")]
    [SerializeField] CustomBulletShooter snakeShooter;
    [SerializeField] float rotationSpeed;
    [SerializeField] float rotationMod;
    [Header("Charge")]
    [SerializeField] int numberOfChargesPerCycle;
    [SerializeField] float timePerCharge;
    [SerializeField] float chargeForce;
    [SerializeField] float timeBetweenCharges;
    [SerializeField] float timeBetweenCycles;

    bool isBigCharging;

    #endregion

    #region SetUp

    protected override void Start()
    {
        base.Start();
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
            // Vector2 point = Random.insideUnitCircle.normalized * Random.Range(minChargeStartingRadius, maxChargeStartingRadius);
            // transform.position = point;
            
            Vector2 chargeDir = Vector3.Normalize(target.position - transform.position);
            
            rb.velocity = Vector2.zero;
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
    #endregion
    
    protected override IEnumerator Knockback(Vector2 dir) { yield return null;}
}