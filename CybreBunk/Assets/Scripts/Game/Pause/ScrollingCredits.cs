using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class ScrollingCredits : MonoBehaviour
{
    [SerializeField] GameObject[]  elements;
    [SerializeField] RectTransform rect;
    [SerializeField] float         cd;
    float                          i;
    void Update()
    {
        Debug.Log(i);
        i++;
        if (i !<= cd) return;
        i = 0;
        Instantiate(elements[ Random.Range(0, elements.Length)], rect.position, quaternion.identity, transform);
    }
}
