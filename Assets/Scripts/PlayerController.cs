using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Gun gun;
    [SerializeField] public float speed = 1.0f;
    [SerializeField] private bool pressYaw;

    
    
    void Update()
    {
        Vector2 axis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        transform.Translate(new Vector3(axis.x,0, axis.y) * speed * Time.deltaTime);
    }
    
}

