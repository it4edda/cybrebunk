using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPLBehaviour : MonoBehaviour
{
    [Header("WORLD")]
    [SerializeField] float topBorder;
    [SerializeField] float botBorder;
    [SerializeField] float leftBorder;
    [SerializeField] float rightBorder;
    [Header("CAR")]
    [SerializeField] int health;
    [SerializeField] float topTurnAcceleration;
    [SerializeField] float topAcceleration;
    [Header("GUNS")]
    [SerializeField] float reloadSpeed;

    Vector3 movementVector;
    float   currentHoriAcceleration;
    float   currentVertAcceleration;
    /*
    TODO:
    add car drift turn,
    add skid-marks,
    make the acc feel good,
    add shooting,
    health and ui,
    
    add horizontal acc
    The closer player gets to left and right walls, the slower the acceleration. CURVE
    */
    void Awake()
    {
    }
    void Start()
    {
        currentHoriAcceleration = topTurnAcceleration;
        currentVertAcceleration = topAcceleration;
    }
    
    void Update()
    {
        //Debug.Log(Input.GetAxis("Vertical"));
        SpeedFunction();
       
    }
    void FixedUpdate()
    {
        transform.position += movementVector;
    }
    void SpeedFunction()
    {
        //add acceleration and shit here instead of start (start stuff is temporary mr big brained guy)
        
        movementVector = new Vector3(Input.GetAxis("Horizontal") * currentVertAcceleration, Input.GetAxis("Vertical") * currentHoriAcceleration);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector2(-10, topBorder), new Vector2(10, topBorder));
        Gizmos.DrawLine(new Vector2(-10, botBorder), new Vector2(10, botBorder));
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(new Vector2(leftBorder, -10), new Vector2(leftBorder, 10));
        Gizmos.DrawLine(new Vector2(rightBorder, -10), new Vector2(rightBorder, 10));
    }
}
