using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    PauseMenu                  pauseMenu;
    PlayerAttack               playerAttack;
    void Start()
    {
        pauseMenu    = FindObjectOfType<PauseMenu>();
        playerAttack = FindObjectOfType<PlayerAttack>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) pauseMenu.DiosBestFriend(!pauseMenu.IsPaused);
        
        if (pauseMenu.IsPaused) return;
        if (Input.GetKey(KeyCode.Mouse0)) playerAttack.Attack();
        //movement input is checked in "PlayerMovement"
        //interact action is checked in "Interaction"'s active function
    }
}
