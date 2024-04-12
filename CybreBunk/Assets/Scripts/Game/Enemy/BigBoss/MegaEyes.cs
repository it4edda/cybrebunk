using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaEyes : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float     speed;
    [SerializeField] float     rotationMod;
    void FixedUpdate()
    {
        Vector3 v2Target = player.position - transform.position;
        float   ang      = Mathf.Atan2(v2Target.y, v2Target.x) * Mathf.Rad2Deg * rotationMod;
        Quaternion q = Quaternion.AngleAxis(ang, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q ,Time.deltaTime * speed);
    }
    
}
