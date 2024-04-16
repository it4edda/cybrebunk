using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : CustomBulletShooter
{
    [Header("General")]
    [SerializeField] bool      hasSword;
    [SerializeField] Transform   weaponGraphics;
    [SerializeField] AudioSource audioSource;
    [SerializeField] float attackSpeed;
    [Header("General Values")]
    
    [Header("Gun")]
    [SerializeField] GameObject gunGraphics;
    [SerializeField] AudioClip  gunSound;
    public List<CustomBulletPattern> addedBulletPaterns = new();
    
    [Header("Sword")]
    [SerializeField] Animator slasher;
    [SerializeField] GameObject swordGraphics;
    [SerializeField] AudioClip  slashSound;
    
    //bool        midAttack = false;
    Camera      cam;
    bool        localCanAttack = true;
    
    public bool CanAttack { get => localCanAttack; set => localCanAttack = value; }
    void Start()
    {
        cam = Camera.main;
        InheritTarotData();
        StartupGraphics();
        
    }
    void InheritTarotData() { hasSword = PlayerManager.selectedCard != null && PlayerManager.selectedCard.swordStart; }
    void StartupGraphics()
    {
        slasher.gameObject.SetActive(false);
        gunGraphics.SetActive(!hasSword);
        swordGraphics.SetActive(hasSword);
    }
    void LateUpdate()
    {
        Aim();
    }
    public void Attack()
    {
        if (!CanAttack) return;
        if (isAttacking) return;
        if(!hasSword) { ChooseNewRoutine(); }
        if (hasSword) { StartCoroutine(Slash());}
    }
    void Aim()
    {
        if (isAttacking) return;
        Vector3 dir   = Input.mousePosition - cam.WorldToScreenPoint(weaponGraphics.position);
        float   angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        weaponGraphics.localRotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    public void SwapWeapon() //ONLY FOR MID RUN DEBUGGING REASONS
    {
        hasSword = !hasSword;
        StartupGraphics();
    }
    IEnumerator Slash()
    {
        isAttacking = true;
        slasher.gameObject.SetActive(true);
        Vector3 a = weaponGraphics.localScale;
        weaponGraphics.localScale = new Vector3(a.x, a.y * -1, a.z);
        audioSource.PlayOneShot(slashSound);
        yield return new WaitForSeconds(slasher.GetCurrentAnimatorStateInfo(0).length);
        slasher.gameObject.SetActive(false);
        isAttacking = false;
    }

    public void ChangePattern(CustomBulletPattern newPattern)
    {
        addedBulletPaterns.Add(newPattern);
        bulletPattern.Add(newPattern);
    }
    public bool HasSword
    {
        get => hasSword;
        set => hasSword = value;
    }
    public float AttackSpeed
    {
        get => timeBetweenPatterns;
        set => timeBetweenPatterns = value;
    }
    
}
