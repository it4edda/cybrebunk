using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ArenaBehaviour : MonoBehaviour
{
    [SerializeField] Vector2 centralPoint;
    [SerializeField] float   suicideDistance;
    float                    calculatedDistance;

    [SerializeField] Transform player;
    
    [SerializeField] Volume    volume;
    Vignette  vignette;

    [SerializeField] Gradient dangerLevel;
    
    void Start()
    {
        volume.profile.TryGet(out vignette);
    }
    
    void Update()
    {
        float dis = Vector2.Distance(centralPoint - (Vector2) player.position, centralPoint);
        float a   = Mathf.InverseLerp(0, suicideDistance, dis >= suicideDistance ? suicideDistance : dis);
        
        vignette.color.value = dangerLevel.Evaluate(a);
        
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(centralPoint, 0.5f);
        Gizmos.DrawWireSphere(centralPoint, suicideDistance);
    }
}
