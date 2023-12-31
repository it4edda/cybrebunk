using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletLogic : DamageDealer
{
    [SerializeField] float speed;
    [SerializeField] float timeToLive;
    protected override void Start()
    {
        timeToLive = PlayerManager.selectedCard.bulletLifetime;
        speed      = PlayerManager.selectedCard.bulletSpeed;
        base.Start();
    }
    void Update()
    {
        transform.localPosition += transform.right * (speed * Time.deltaTime);
        timeToLive -= Time.deltaTime;
        if (timeToLive <= 0) Destroy(gameObject);
    }
}
