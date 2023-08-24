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
    
    bool                        midAttack;

    Camera cam;
    void Start()
    {
        InheritTarotData();
        
                
        slasher.gameObject.SetActive(false);
        gunGraphics.SetActive(!hasSword);
        swordGraphics.SetActive(hasSword);
        
        cam = Camera.main;
        
    }
    void InheritTarotData()
    {
        hasSword = PlayerManager.selectedCard.swordStart;

    }
    void StartupGraphics()
    {
        
    }
    void Update()
    {
        Aim();

        // ReSharper disable once InvertIf
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (!hasSword)
            {
                if (!midAttack) StartCoroutine(Shoot());
                return;
            }

            if (!midAttack) StartCoroutine(Slash());
        }
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
        Start();
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
        // instantiate bullet 
        //aim bullet
        //return
    }

    //TODO Rotate weapon at mouse, shoot at mouse, deal damage
}
