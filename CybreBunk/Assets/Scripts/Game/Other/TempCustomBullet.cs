using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempCustomBullet : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float timeToLive;
    void Start()
    {
        timeToLive = PlayerManager.selectedCard.bulletLifetime;
        speed      = PlayerManager.selectedCard.bulletSpeed;
    }
    void Update()
    {
        transform.localPosition += transform.right * (speed * Time.deltaTime);
        timeToLive -= Time.deltaTime;
        if (timeToLive <= 0) Destroy(gameObject);
    }
}
