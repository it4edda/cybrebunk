using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("Mortality fields")]
    [SerializeField] int health;
    [SerializeField] bool         isBig = false;
    [SerializeField] GameObject[] smallBlood;
    [SerializeField] GameObject[] bigBlood;
    // Start is called before the first frame update
    void Start()
    {
        //todo find player and move towards him ( basic movement for father script)
        //todo be able to take damage and die
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void TakeDamage()
    {
        
    }
    void FixedUpdate()
    {
        Movement();
    }
    protected virtual void Movement()
    {
        
    }
}
