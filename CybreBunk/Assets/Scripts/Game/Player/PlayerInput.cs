using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    Vector2                    moveInput;
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
        //interact action is checked in "Interaction"'s active function
    }
}
