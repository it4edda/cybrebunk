using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletLogic : DamageDealer
{
    [SerializeField] float speed;
    [SerializeField] float timeToLive;
    [SerializeField] bool usePlayerRange;
    protected override void Start()
    {
        if(isAllied && usePlayerRange)
        {
            timeToLive *= PlayerManager.selectedCard.playerRange;
            speed      = PlayerManager.selectedCard.bulletSpeed * speed;
        }
        base.Start();
    }
    void Update()
    {
        transform.localPosition += transform.right * (speed * Time.deltaTime);
        timeToLive -= Time.deltaTime;
        if (timeToLive <= 0) Destroy(gameObject);
    }
}
