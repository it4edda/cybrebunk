using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Weight"), SerializeField]
     float triggerSpeed;
    [SerializeField] bool hasSword;

    [Header("Graphics"), SerializeField]
     Transform weaponGraphics;
    [SerializeField] Animator slasher;
    bool                      midSlash;
    void Start()
    {
        slasher.gameObject.SetActive(false);
    }
    void Update()
    {
        Aim();

        // ReSharper disable once InvertIf
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (!hasSword)
            {
                Shoot();
                return;
            }

            if (!midSlash) StartCoroutine(Slash());
        }
    }
    void Aim()
    {
        Vector3 dir   = Input.mousePosition - Camera.main.WorldToScreenPoint(weaponGraphics.position);
        float   angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        weaponGraphics.localRotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    IEnumerator Slash() //DO BOXCOLLISION TO FIND ENEMY IN RANGE
    {
        midSlash = true;
        slasher.gameObject.SetActive(true);
        Vector3 a = weaponGraphics.localScale;
        weaponGraphics.localScale = new Vector3(a.x, a.y * -1, a.z);
        yield return new WaitForSeconds(slasher.GetCurrentAnimatorStateInfo(0).length);
        slasher.gameObject.SetActive(false);
        midSlash = false;
    }
    void Shoot()
    {
        //PANG PANG 
    }

    //TODO Rotate weapon at mouse, shoot at mouse, deal damage
}
