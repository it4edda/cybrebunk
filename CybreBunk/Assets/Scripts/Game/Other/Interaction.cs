using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] float          radius;
    [SerializeField] bool           canInteract;
    [SerializeField] ParticleSystem particles;
    [SerializeField] Animator       interact;
    Transform                       target;
    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerMovement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (particles.isEmitting != canInteract)
        {
            if (canInteract) particles.Play();
            else particles.Stop();
        }

        bool a = Vector2.Distance(transform.position, target.position) < radius;
        interact.SetBool("Showing", a);
        
        if (canInteract && a)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("space");
                canInteract = false;
            }
            return;
        }
    }
    void OnDrawGizmosSelected()
    {
     Gizmos.color = Color.red;
     Gizmos.DrawWireSphere(transform.position, radius);
    }
}
