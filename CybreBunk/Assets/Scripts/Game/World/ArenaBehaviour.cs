using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ArenaBehaviour : MonoBehaviour
{
    [SerializeField] Vector2   centralPoint;
    [SerializeField] float     suicideDistance;
    [SerializeField] float     suicideDistanceWhileBossIsAlive;
    float                      calculatedDistance;
    public bool                bossIsAlive = false;
    [SerializeField] Transform player;
    
    [SerializeField] Volume volume;
    PlayerCamera            cam;
    Vignette                vignette;

    [SerializeField] Gradient dangerLevel;
    float                     timeOutsideDistance   = 0f;
    [SerializeField] float     gameOverTimeThreshold = 5f;
    
    void Start()
    {
        cam = FindObjectOfType<PlayerCamera>();
        volume.profile.TryGet(out vignette);
    }
    
    void Update()
    {
        float savedDistance = bossIsAlive ? suicideDistanceWhileBossIsAlive : suicideDistance;
        
        float dis = Vector2.Distance(centralPoint - (Vector2) player.position, centralPoint);
        float a   = Mathf.InverseLerp(0, savedDistance, dis >= savedDistance ? savedDistance : dis);
        
        vignette.color.value = dangerLevel.Evaluate(a);
        
        if (dis > savedDistance)
        {
            timeOutsideDistance += Time.deltaTime;

            if (timeOutsideDistance >= gameOverTimeThreshold)
            {
                player.GetComponent<PlayerStats>().Health =- 100;
            }
        }
        else
        {
            timeOutsideDistance = 0f;
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(centralPoint, 0.5f);
        Gizmos.DrawWireSphere(centralPoint, suicideDistance);
        
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(centralPoint, suicideDistanceWhileBossIsAlive);
    }
}
