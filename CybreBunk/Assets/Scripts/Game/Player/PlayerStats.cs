using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    //attack speed, movement speed, 
    [Header("values")]
    [SerializeField] int maxHealth;
    [SerializeField] float movementSpeed;
    
    [Header("Particles")]
    [SerializeField] ParticleSystem damageParticles;
    [SerializeField] ParticleSystem deathParticles;
    //[SerializeField] ItemInven      itemInven;

    bool isDead;
    bool canDie = true;
    int  health;
    int  damage;

    SceneTransitions    transitions;
    UserInterfaceHealth uiHealth;
    PlayerMovement      movement;
    PlayerAttack        attack;
    PlayerCamera        cam;
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
        set => maxHealth += value;
    }
    public int Health
    {
        get => health;
        set => health = health > maxHealth ? maxHealth : UpdateHealth(value);
    }
    int UpdateHealth(int value)
    {
        if (isDead) return health;
        PlayerInventory.instance.TakingDamage();
        Debug.Log("WHY DOES THIS ERRORRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR");
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
        StartCoroutine(transitions.Transition("Death"));
        //SceneManager.LoadScene("Death");
    }
    #endregion
    public int Damage
    {
        get => damage;
        set => damage = damage <= 0 ? damage = 1 : damage += value;
    }
#region MovementSpeed
    public float MovementSpeed
    {
        get => movementSpeed;
        set => movementSpeed += UpdateMovement(value);
    }
    float UpdateMovement(float value)
    {
        movementSpeed      = math.abs(movementSpeed * value);
        movement.MoveSpeed = Vector2.one * movementSpeed;
        return value;
    }
#endregion
    
    public void GodMode() //use for debugging (most of the time)
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
        
        UpdateMovement(data.startingMovementSpeed);
        
        MaxHealth = data.debugTool ? 30 : data.startingHealth;
    }
#endregion
}
