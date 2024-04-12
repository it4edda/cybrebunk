using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CustomBulletShooter : MonoBehaviour
{
    [SerializeField] List<CustomBulletPattern> bulletPattern = new();
    [SerializeField] float timeBetweenPatterns; 
    [SerializeField] Transform firePos;

    void Start()
    {
        ChooseNewRoutine();
    }

    IEnumerator Shooting(CustomBulletPattern pattern)
    {
        foreach (CustomBulletPattern.PatternRows row in pattern.pattern)
        {
            foreach (CustomBulletPattern.BulletsInRows bullets in row.bulletsToFire)
            {
                Vector3 newRotation = new Vector3(firePos.rotation.x, firePos.rotation.y,
                    firePos.rotation.y - +bullets.angle);
                Instantiate(bullets.bullet, firePos.position, Quaternion.Euler(newRotation));
            }
            yield return new WaitForSeconds(row.secondsUntilNextRowFire);
        }

        yield return new WaitForSeconds(timeBetweenPatterns);
        ChooseNewRoutine();
    }

    void ChooseNewRoutine()
    {
        StartCoroutine(Shooting(bulletPattern[Random.Range(0, bulletPattern.Count)]));
    }
}
