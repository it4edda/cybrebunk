using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    //attack speed, movement speed, 
    [Header("Values")]
    [SerializeField] int            startingHealth;
    [SerializeField] int            startingDamage;
    
    [Header("Particles")]
    [SerializeField] ParticleSystem damageParticles;
    [SerializeField] ParticleSystem deathParticles;
    //[SerializeField] ItemInven      itemInven;
    
    bool                isDead = false;
    bool                canDie = true;
    int                 health;
    int                 damage;
    UserInterfaceHealth uiHealth;
    PlayerMovement      movement;
    PlayerAttack        attack;
    PlayerCamera        cam;
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
        cam      = FindObjectOfType<PlayerCamera>();
        movement = GetComponent<PlayerMovement>();
        attack   = GetComponent<PlayerAttack>();
        uiHealth = FindObjectOfType<UserInterfaceHealth>();
        uiHealth.SetMaxHealth(startingHealth);
    }
    int UpdateHealth(int value)
    {
        if (isDead) return health;
        damageParticles.Play();
        cam.CameraShake(0.3f);
        uiHealth.ModifyHealth(value);
        if (health + value <= 0) Death();
        return health += value;
    }
    void Death()
    {
        if (canDie) StartCoroutine(DeathAction());
        Debug.Log("death triggered");
    }
    IEnumerator DeathAction()
    {
        isDead           = true;
        movement.CanMove = false;
        attack.CanAttack = false;
        deathParticles.Play();
        yield return new WaitForSeconds(deathParticles.main.duration + 0.5f);
        SceneManager.LoadScene("Death");
    }
    public void GodMode() //use for debugging (most of the time)
    {
        canDie = !canDie; 
        Debug.Log("GOD MODE =" + !canDie);
    }
}



/// <summary>
/// Basically the inventory of the player. All the items the player picks up gets placed in this struct.
/// </summary>
// [Serializable]
// public struct ItemInven //delve more into this
// {
//     public AttackItemData[]       attackItemData;
//     public MovementItemData[]     movementItemData;
//     public SpecialItemData[]      specialItemData;
//     public AmalgamationItemData[] amalgamationItemData;
// }
