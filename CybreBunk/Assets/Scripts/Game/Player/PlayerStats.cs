using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    //attack speed, movement speed, 
    [SerializeField] int startingHealth;
    [SerializeField] int startingDamage;
    int                  health;
    int                  damage;
    UserInterfaceHealth  uiHealth;
    public int Health
    {
        get => health;
        set => health = //health <= 0              ? Death() :
                        health >  startingHealth ? startingHealth            : UpdateHealth(value);
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
    void Start()
    {
        uiHealth = FindObjectOfType<UserInterfaceHealth>();
        uiHealth.SetMaxHealth(startingHealth);
    }
    
    int UpdateHealth(int value)
    {
        uiHealth.ModifyHealth(value);
        if (health + value <= 0) Death();
        return health += value;
    }
    int Death()
    {
        //do dying stuff here
        Debug.Log("i have successfully died");
        if (PlayerManager.selectedCard.debugTool) return health;
        //SceneManager.LoadScene("Deity");
        
        //add animation
        return health;
    }

}
