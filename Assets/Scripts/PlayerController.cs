using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Gun gun;

    void Start()
    {
        // Get the Gun component attached to the player’s hand
        gun = GetComponentInChildren<Gun>();  // Assuming the gun is a child of the player
    }

    void Update()
    {
        // Check if the player presses the fire button (VR controller input mapped to Fire1)
        if (Input.GetButtonDown("Fire1") && !gun.hasFired)
        {
            gun.PlayerShoot();  // Call the player-specific shoot method
        }
    }
}

