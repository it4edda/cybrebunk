using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterfaceDebugging : MonoBehaviour
{
    [SerializeField] GameObject[] debuggingElements;
    void Start()
    {
        Debug.Log(PlayerManager.selectedCard);
        foreach (GameObject t in debuggingElements) 
        { t.SetActive(PlayerManager.selectedCard && PlayerManager.selectedCard.debugTool); }
    }
}