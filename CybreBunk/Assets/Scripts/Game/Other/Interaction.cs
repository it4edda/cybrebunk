using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] float          radius;
    [SerializeField] protected bool canInteract;
    [SerializeField] Animator       interactIcon;
    Transform                       target;
    protected virtual void Start()
    {
        target = FindObjectOfType<PlayerMovement>().transform;
    }
    void Update()
    {
        InteractionPassive();

        bool a = Vector2.Distance(transform.position, target.position) < radius;
        interactIcon.SetBool("Showing", a);
        
        if (canInteract && a)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                InteractionActive();
            }
        }
    }
    protected virtual void InteractionPassive()
    {
        
    }
    protected virtual void InteractionActive()
    {
        canInteract = false;
    }
    void OnDrawGizmosSelected()
    {
     Gizmos.color = Color.red;
     Gizmos.DrawWireSphere(transform.position, radius);
    }
}
