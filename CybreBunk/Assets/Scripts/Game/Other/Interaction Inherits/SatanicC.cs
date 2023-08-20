using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatanicC : Interaction
{
    
    [SerializeField] ParticleSystem particles;
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
        base.InteractionActive();
    }
    void Suction()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
             //transform.GetChild(i).transform.position
        }
    }
}
