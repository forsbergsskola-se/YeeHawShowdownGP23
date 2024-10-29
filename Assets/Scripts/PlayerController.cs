using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera vrCamera; // Assign the main VR camera in the Inspector
    public float speed = 2.0f; // Movement speed

    public void MovementInput()
    {
        Vector2 axis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

        // Calculate the movement direction based on the camera's forward and right directions
        Vector3 moveDirection = (vrCamera.transform.right * axis.x + vrCamera.transform.forward * axis.y);
        moveDirection.y = 0; // Ensure no vertical movement

        // Move the player in the calculated direction, relative to the world
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
    }

}

