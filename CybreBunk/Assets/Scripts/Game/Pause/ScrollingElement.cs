using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingElement : MonoBehaviour
{
    [SerializeField] float lowestAllowedPosition;
    [SerializeField] float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * speed ;
        if(transform.position.y < lowestAllowedPosition) Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(new Vector3(-100, lowestAllowedPosition), new Vector3(100, lowestAllowedPosition));
    }
}
