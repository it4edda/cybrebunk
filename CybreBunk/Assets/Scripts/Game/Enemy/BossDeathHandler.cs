using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeathHandler : MonoBehaviour
{
    [SerializeField] ParticleSystem particles;
    public void Dying()
    {
        particles.Play();
    }
    public void Death()
    {
        Destroy(gameObject);
    }
}
