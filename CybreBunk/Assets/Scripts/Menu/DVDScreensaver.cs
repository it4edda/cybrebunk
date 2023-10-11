using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class DVDScreensaver : MonoBehaviour
{
    [SerializeField] float         force = 1f;
    [SerializeField] RectTransform rect;
    TMP_Text                       text;
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.one * force);
        text       = rect.gameObject.GetComponent<TMP_Text>();
    }
    void Update() => rect.position = transform.position;
    void OnCollisionEnter2D(Collision2D other)
    {
        text.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
        //text.color = new Color(Random.Range(0, 255), Random.Range(0, 255), Random.Range(0, 255));
    }
}
