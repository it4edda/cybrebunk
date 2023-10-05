using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class SatanicC : Interaction
{
    [SerializeField] ParticleSystem particles;
    [SerializeField] ParticleSystem bloodTrail;
    [SerializeField] Animator       bloodGround;
    Transform                       bloodParent;
    UserInterfaceGauge              gauge;
    protected override void Start()
    {
        base.Start();
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
        bloodGround.gameObject.SetActive(true);
        gauge.UpdateGaugeSlider(-gauge.MaxValue);
        base.InteractionActive();
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
