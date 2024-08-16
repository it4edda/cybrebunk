using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryManager : MonoBehaviour
{
    [SerializeField] GameObject victoryCanvas;
    private bool hasClicked = false;
    
    PauseMenu pauseMenu;
    private void Start()
    {
        pauseMenu = FindObjectOfType<PauseMenu>();
        PlayerManager.selectedCard.totalWins++;
    }

    public void ToggleVictory(bool freeze)
    {
        victoryCanvas.SetActive(freeze);
        Debug.Log("set music change here + sound effects");
        pauseMenu.TimeFreeze(freeze);
         
    }
    public void ExitToMenu()
    {
        if (hasClicked) return;
        hasClicked = true;
        pauseMenu.TimeFreeze(false);
        StartCoroutine(FindObjectOfType<SceneTransitions>().Transition("Deity"));
    }
}
