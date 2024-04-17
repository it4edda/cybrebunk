using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Turret : CustomBulletShooter
{
    [SerializeField] float range;
    [SerializeField] float rotationSpeed;
    [SerializeField] float rotationMod;
    [SerializeField] float timer;

    //TODO make into item and maybe set in time limit
    Transform nearestEnemy;
    PlayerAttack playerAttack;

    void Start()
    {
        playerAttack = FindObjectOfType<PlayerAttack>();
        ChoosePattern();
    }

    void ChoosePattern()
    {
        if (FindObjectOfType<PlayerAttack>().addedBulletPaterns.Count <= 0) return;
        foreach (CustomBulletPattern pattern in FindObjectOfType<PlayerAttack>().addedBulletPaterns)
        {
            bulletPattern.Add(pattern);
        }
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            //TODO object pool
            Destroy(gameObject);
        }
        //TODO if buggy fix this
        if (!playerAttack.canTurretAttack) { return; }
        
        if (FindNearestEnemy())
        {
            Vector3 v2Target = nearestEnemy.position - transform.position;
            float   ang      = Mathf.Atan2(v2Target.y, v2Target.x) * Mathf.Rad2Deg * rotationMod;
            Quaternion q = Quaternion.AngleAxis(ang, Vector3.forward);
            firePos.rotation = Quaternion.Slerp(transform.rotation, q ,Time.deltaTime * rotationSpeed);

            ChooseNewRoutine();
        }
    }

    Transform FindNearestEnemy()
    {
        List<EnemyBehaviour> availableEnemies = new List<EnemyBehaviour>(FindObjectsOfType<EnemyBehaviour>().Where(enemy => Vector3.Distance(transform.position, enemy.gameObject.transform.position) <= range));
        if (availableEnemies.Count <= 0)
        {
            nearestEnemy = null;
            return nearestEnemy;
        }
        foreach (EnemyBehaviour enemy in availableEnemies)
        {
            if (nearestEnemy == null)
            {
                nearestEnemy = enemy.transform;
            }else if (Vector3.Distance(transform.position, enemy.transform.position) < Vector3.Distance(transform.position, nearestEnemy.position))
            {
                nearestEnemy = enemy.transform;
            }
        }
        return nearestEnemy;
    }
}
