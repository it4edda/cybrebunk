using System;
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
    [SerializeField] Animator       exclamation;
    [SerializeField] AudioClip      continuationSound;
    [SerializeField] AudioClip      finalBossSound;
    [SerializeField] GameObject[]   bosses;
    [SerializeField] Transform      centrePoint;
    Transform                       bloodParent;
    ArenaBehaviour                  arena;
    UserInterfaceGauge              gauge;
    AudioSource                     audioSource;
    EnemySpawning                   enemySpawning;
    MusicPlayer                     musicPlayer;
    public int                      timesUsed;
    public bool                     canConsume = true;
    protected override void Start()
    {
        base.Start();
        canConsume    = true;
        arena         = FindObjectOfType<ArenaBehaviour>();
        enemySpawning = FindObjectOfType<EnemySpawning>();
        audioSource   = GetComponent<AudioSource>();
        bloodParent   = transform.Find("BloodParent");
        gauge         = FindObjectOfType<UserInterfaceGauge>();
        musicPlayer   = FindObjectOfType<MusicPlayer>();
    }
    protected override void InteractionPassive()
    {
        if (particles.isEmitting != canInteract)
        {
            if (canInteract)
                particles.Play();
            
            else particles.Stop();
            
            exclamation.SetBool("Show", canInteract);
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
                BossSpawning(continuationSound);
                Instantiate(bosses[0]);
                break;
            
            case 2:
                BossSpawning(continuationSound); 
                Instantiate(bosses[1], centrePoint.position, quaternion.identity);
                break;
                
            case 3:
                BossSpawning(finalBossSound);
                Instantiate(bosses[2]);
                break;
            
            default:
                Debug.Log("ey");
                break;
                
        }
    }
    void BossSpawning(AudioClip clip)
    {
        FindObjectOfType<PlayerCamera>().SetStationary();
        musicPlayer.ChangeMusic(musicPlayer.allSong[4]);
        arena.bossIsAlive = true;
        audioSource.PlayOneShot(clip);
        canConsume             = false;
        enemySpawning.CanSpawn = false;
    }
    public void BossDeath()
    {
        musicPlayer.ChangeMusic(musicPlayer.allSong[1]);
        canConsume        = true;
        arena.bossIsAlive = false;
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
    public void ResetSatan()   => canInteract = true;
}
