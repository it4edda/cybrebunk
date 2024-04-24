using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RippleManager : MonoBehaviour
{

    [SerializeField] float duration = 0.75f;
    Material               mat;
    static int             distance = Shader.PropertyToID("DistanceFromCenter");
    void Awake()
    {
        mat = GetComponent<SpriteRenderer>().material;
    }
    void Start()
    {
        StartCoroutine(Ripple());
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))StartCoroutine(Ripple());
    }
    IEnumerator Ripple()
    {
        mat.SetFloat(distance, 0.5f);

        float lerped  = 0f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            lerped  =  Mathf.Lerp(-0.1f, 1f, (elapsed / duration));
            
            mat.SetFloat(distance, lerped);
            yield return null;
        }
        //Destroy(gameObject);
    }
}
