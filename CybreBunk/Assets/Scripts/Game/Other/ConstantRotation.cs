using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotation : MonoBehaviour
{
    [SerializeField] float speed;
    void Update()
    {
        transform.Rotate(transform.forward, speed * Time.deltaTime);
    }
}
