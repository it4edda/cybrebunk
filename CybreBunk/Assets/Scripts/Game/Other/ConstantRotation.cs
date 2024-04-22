using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotation : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] bool  isRect;
    RectTransform          tran;
    void Start()
    {
        if (isRect) GetComponent<RectTransform>();
    }
    void Update()
    {
        if (tran) tran.Rotate(transform.forward, speed * Time.deltaTime);
        else transform.Rotate(transform.forward, speed * Time.deltaTime);
    }
}
