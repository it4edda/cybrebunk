using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CustomBulletShooter : MonoBehaviour
{
    [Header("Bullet Pattern")]
    [SerializeField] protected List<CustomBulletPattern> bulletPattern = new();
    [SerializeField] bool fireContinuously;
    [SerializeField] protected float timeBetweenPatterns; 
    [SerializeField] public Transform firePos;
    public bool isAttacking = false;
    
    IEnumerator Shooting(CustomBulletPattern pattern)
    {
        isAttacking = true;
        foreach (CustomBulletPattern.PatternRows row in pattern.pattern)
        {
            foreach (CustomBulletPattern.BulletsInRows bullets in row.bulletsToFire)
            {
                Vector3 newRotation = new(firePos.rotation.eulerAngles.x, firePos.rotation.eulerAngles.y,
                    firePos.rotation.eulerAngles.z + bullets.angle);
                Instantiate(bullets.bullet, firePos.position, Quaternion.Euler(newRotation));
            }
            yield return new WaitForSeconds(row.secondsUntilNextRowFire);
        }

        yield return new WaitForSeconds(timeBetweenPatterns);
        isAttacking = false;
        if (fireContinuously)
        {
            ChooseNewRoutine();
        }
    }

    public void ChooseNewRoutine()
    {
        if (isAttacking) { return; }
        
        StartCoroutine(Shooting(bulletPattern[Random.Range(0, bulletPattern.Count)]));
    }
}
