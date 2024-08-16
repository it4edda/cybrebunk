using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PlayerStats : MonoBehaviour
{
    //attack speed, movement speed, 

    #region Variables
    [Header("values")]
    [SerializeField] int maxHealth;
    [SerializeField] float movementSpeed;
    [SerializeField] float range;
    [SerializeField] private float invincibilityTime;
    
    
    [Header("Particles")]
    [SerializeField] ParticleSystem damageParticles;
    [SerializeField] ParticleSystem deathParticles;
    //[SerializeField] ItemInven      itemInven;

    [Header("Audio")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip damageSound;

    [Header("Debug")]
    [SerializeField] int increaseInBloodGain;
    
    bool isDead;
    bool canDie = true;
    int  health;
    int  damage;

    SceneTransitions    transitions;
    UserInterfaceHealth uiHealth;
    PlayerMovement      movement;
    PlayerAttack        attack;
    PlayerCamera        cam;
    #endregion
    void Start()
    {
        cam         = FindObjectOfType<PlayerCamera>();
        transitions = FindObjectOfType<SceneTransitions>();
        movement    = GetComponent<PlayerMovement>();
        attack      = GetComponent<PlayerAttack>();
        
        ImportStats();
        
        health = maxHealth;
        uiHealth = FindObjectOfType<UserInterfaceHealth>();
        uiHealth.SetMaxHealth(maxHealth);
    }
    
#region Practical HealthRelated
    #region Health
    public int MaxHealth
    {
        get => maxHealth;
        set => maxHealth = value;
    }
    public int Health
    {
        get => health;
        set => health = health > maxHealth ? maxHealth : UpdateHealth(value);
    }
    int UpdateHealth(int value)
    {
        if (isDead || !canDie) return health;
        audioSource.PlayOneShot(damageSound);
        PlayerInventory.instance.TakingDamage();
        damageParticles.Play();
        cam.CameraShake(0.3f);
        uiHealth.ModifyHealth(value);
        Debug.Log("Took " + value + "damage");
        if (health + value <= 0) Death();
        StartCoroutine(Invincibility());
        return health += value;
    }
    void Death()
    {
        if (canDie) StartCoroutine(DeathAction());
    }
    IEnumerator DeathAction()
    {
        isDead                                   = true;
        movement.CanMove                         = false;
        attack.CanAttack                         = false;
        deathParticles.Play();
        
        audioSource.PlayOneShot(deathSound);
        var a = FindObjectOfType<MusicPlayer>();
        a.ChangeMusic(a.allSong[3]);
        
        yield return new WaitForSeconds(deathParticles.main.duration + 0.5f);
        StartCoroutine(transitions.Transition("Death"));
    }
    
    #endregion
    public int Damage
    {
        get => damage;
        set => damage = damage <= 0 ? damage = 1 : damage = value;
    }

    public float Range               { get => range; set => range = IncreaseRange(value); }
    float IncreaseRange(float val)
    {
        return val;
    }
    public int   IncreaseInBloodGain { get => increaseInBloodGain; set => increaseInBloodGain = value; }

    #region MovementSpeed
    public float MovementSpeed
    {
        get => movementSpeed;
        set => movementSpeed = UpdateMovement(value);
    }
    float UpdateMovement(float value)
    {
        movementSpeed      = math.abs(value);
        movement.MoveSpeed = Vector2.one * movementSpeed;
        return value;
    }
    
#endregion
IEnumerator Invincibility()
{
    GodMode(true);
    yield return new WaitForSeconds(invincibilityTime);
    GodMode(false);
}
    public void GodMode(bool value)
    {
        canDie = !value; 
    }
    public void GodMode()
    {
        canDie = !canDie; 
        Debug.Log("GOD MODE =" + !canDie);
    }
#endregion
#region Inport stats
    void ImportStats()
    {
        TarotData data = PlayerManager.selectedCard;

        Damage             = data.startingDamage;
        attack.AttackSpeed = data.startingAttackSpeed; //NOT UPDATED TO DAILY STANDARD
        range = data.playerRange;
        
        UpdateMovement(data.startingMovementSpeed);
        
        MaxHealth = data.debugTool ? 30 : data.startingHealth;
    }
#endregion
}
