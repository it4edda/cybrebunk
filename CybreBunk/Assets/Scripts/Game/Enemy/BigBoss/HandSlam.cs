using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSlam : CustomBulletShooter
{
    Animator animator;
    void Start() => animator = GetComponent<Animator>();
    public void StartSlam() => animator.SetTrigger("Slam");
    public void FireSlamBullets() => ChooseNewRoutine();
}
