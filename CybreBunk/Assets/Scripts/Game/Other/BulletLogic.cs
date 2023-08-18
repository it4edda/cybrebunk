using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : DamageDealer
{
    [SerializeField] float speed;
    [SerializeField] float timeToLive;
    void Update()
    {
        transform.localPosition += transform.right * (speed * Time.deltaTime);
        timeToLive -= Time.deltaTime;
        if (timeToLive <= 0) Destroy(gameObject);
        
    }
}
