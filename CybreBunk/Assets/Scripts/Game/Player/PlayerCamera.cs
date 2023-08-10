using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] Transform followPoint;
    void FixedUpdate()
    {
        transform.position = new Vector3(followPoint.position.x, followPoint.position.y , -10);
        //smoothen this
    }
}
