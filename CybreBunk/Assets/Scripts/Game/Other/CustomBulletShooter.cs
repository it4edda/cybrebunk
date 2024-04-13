using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CustomBulletShooter : MonoBehaviour
{
    [SerializeField] List<CustomBulletPattern> bulletPattern = new();
    [SerializeField] float timeBetweenPatterns; 
    [SerializeField] protected Transform firePos;
    bool isFiring;
    
    IEnumerator Shooting(CustomBulletPattern pattern)
    {
        isFiring = true;
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
        isFiring = false;
    }

    protected void ChooseNewRoutine()
    {
        if (isFiring) { return; }
        
        StartCoroutine(Shooting(bulletPattern[Random.Range(0, bulletPattern.Count)]));
    }
}
