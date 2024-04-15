using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class SatanicC : Interaction
{
    [SerializeField] ParticleSystem particles;
    [SerializeField] ParticleSystem bloodTrail;
    [SerializeField] Animator       bloodGround;
    [SerializeField] AudioClip      continuationSound;
    [SerializeField] AudioClip      finalBossSound;
    [SerializeField] GameObject[]   bosses;
    Transform                       bloodParent;
    UserInterfaceGauge              gauge;
    AudioSource                     audioSource;
    public int                             timesUsed;
    protected override void Start()
    {
        base.Start();
        audioSource = GetComponent<AudioSource>();
        bloodParent = transform.Find("BloodParent");
        gauge       = FindObjectOfType<UserInterfaceGauge>();
    }
    protected override void InteractionPassive()
    {
        if (particles.isEmitting != canInteract)
        {
            if (canInteract) particles.Play();
            else particles.Stop();
        }
    }
    protected override void InteractionActive()
    {
        Suction();
        timesUsed++;
        gauge.UpdateGaugeSlider(-gauge.MaxValue);
        base.InteractionActive();

        switch (timesUsed)
        {
            case <= 1:
                audioSource.PlayOneShot(continuationSound);
                Debug.Log("SPAWN BOSS 1");
                FindObjectOfType<PlayerCamera>().SetStationary();
                Instantiate(bosses[0]);
                break;
            
            case 2:
                audioSource.PlayOneShot(continuationSound);
                Debug.Log("SPAWN BOSS 2");
                break;
                
            case 3:
                Debug.Log("FINAL BOSS");
                audioSource.PlayOneShot(finalBossSound);
                FindObjectOfType<PlayerCamera>().SetStationary();
                Instantiate(bosses[2]);
                break;
            
            default:
                Debug.Log("NO MORE BOSSES");
                break;
                
        }
    }
    public void CallBloodGround()
    {
        bloodGround.gameObject.SetActive(true);
    }
    void Suction()
    {
        for (int i = bloodParent.childCount; i > 0; i--)
        {
             var a = bloodParent.GetChild(i - 1).gameObject;
             Instantiate(bloodTrail, a.transform.position , quaternion.identity);
             Destroy(a);
        }
        //Debug.Log(bloodParent.childCount);
    }
    public void ResetSatan() => canInteract = true;
}
