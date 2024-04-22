using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UserInterfacePopUp : MonoBehaviour
{
    [SerializeField] float durationOnScreen;
    [SerializeField] Color baseColor;
    [SerializeField] Color bossColor;
    TextMeshProUGUI        text;
    Animator               animator;
    void Start()
    {
        text     = GetComponent<TextMeshProUGUI>();
        animator = GetComponent<Animator>();
    }
    
    public void CallPopUp(string text, bool isBoss) =>
        StartCoroutine(PopUp(text, isBoss));
    
    IEnumerator PopUp(string text, bool isBoss)
    {
        yield return new WaitUntil(() => !animator.GetBool("Show"));
        this.text.color = isBoss ? bossColor : baseColor;
        this.text.text  = text; //HAHA NILS LOOK AT THIS
        
        animator.SetBool("Show", true);
        yield return new WaitForSeconds(durationOnScreen);
        animator.SetBool("Show", false);
    }
}
