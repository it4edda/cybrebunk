using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    //attack speed, movement speed, 
    [SerializeField] int startingHealth;
    [SerializeField] int startingDamage;
    int                  health;
    int                  damage;
    public int Health
    {
        get => health;
        set => health = health <= 0              ? Death() :
                        health >  startingHealth ? startingHealth            : health += value;
    }
    public int Damage
    {
        get => damage;
        set => damage = damage <= 0 ? damage = 1 : damage += value;
    }
    void Awake()
    {
        health = startingHealth;
        damage = startingDamage;
    }
    int Death()
    {
        //do dying stuff here
        Debug.Log("i have successfully died");
        return 0;
    }

}
