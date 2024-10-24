using UnityEngine;  // Unity's core library for MonoBehaviour, GameObjects, etc.

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;  // Reference to the bullet prefab
    public Transform firePoint;      // The point on the gun where the bullet will be fired from
    public float bulletForce = 20f;  // Speed or force at which the bullet will be shot

    private bool hasFired = false;   // Boolean to keep track of whether the gun has fired already (one bullet per game)

    void Start()
    {
        // Make sure the gun hasn't fired when a new game starts
        hasFired = false;
    }

    void Update()
    {
        // Check if the player presses the fire button (or if AI triggers it) and if the gun hasn't fired yet
        if (Input.GetButtonDown("Fire1") && !hasFired)
        {
            Shoot();  // Call the Shoot function if conditions are met
        }
    }

    // Function to handle shooting the gun
    public void Shoot()
    {
        // If the gun has already fired, do nothing
        if (hasFired) return;

        hasFired = true;  // Set the gun as having fired (so it can't fire again)

        // Instantiate (create) a new bullet at the firePoint position and orientation (rotation)
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Get the Rigidbody component attached to the bullet (needed for applying physics)
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        // Apply force to the bullet to make it move forward. 
        // The direction is the forward direction of the firePoint, multiplied by the bulletForce value
        rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);  // Impulse applies the force instantly
    }
}

