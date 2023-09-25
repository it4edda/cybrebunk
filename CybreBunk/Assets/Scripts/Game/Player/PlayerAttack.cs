using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("General")]
    [SerializeField] bool      hasSword;
    [SerializeField] Transform weaponGraphics;
    
    [Header("Gun")]
    [SerializeField] float triggerSpeed;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject gunGraphics;
    
    [Header("Sword")]
    [SerializeField] Animator slasher;
    [SerializeField] GameObject swordGraphics;
    
    bool        midAttack = false;
    Camera      cam;
    bool        canAttack = true;
    
    public bool CanAttack { get => canAttack; set => canAttack = value; }
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
        if (midAttack) return;
        StartCoroutine(!hasSword ? Shoot() : Slash());
    }
    void Aim()
    {
        if (midAttack) return;
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
        midAttack = true;
        slasher.gameObject.SetActive(true);
        Vector3 a = weaponGraphics.localScale;
        weaponGraphics.localScale = new Vector3(a.x, a.y * -1, a.z);
        yield return new WaitForSeconds(slasher.GetCurrentAnimatorStateInfo(0).length);
        slasher.gameObject.SetActive(false);
        midAttack = false;
    }
    IEnumerator Shoot()
    {
        midAttack = true;
        Instantiate(bulletPrefab, transform.position, weaponGraphics.localRotation);
        yield return new WaitForSeconds(triggerSpeed);
        midAttack = false;
    }
}
