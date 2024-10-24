using UnityEngine;  // Unity's core library for MonoBehaviour, GameObjects, etc.

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;  // Reference to the bullet prefab
    public Transform firePoint;      // The point on the gun where the bullet will be fired from
    public float bulletForce = 20f;  // Speed or force at which the bullet will be shot

    public bool hasFired = false;   // Boolean to keep track of whether the gun has fired already (one bullet per game)

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
            PlayerShoot();  // Call the Shoot function if conditions are met
        }
    }
    
    
    public void PlayerShoot()
    {
        if (hasFired) return;  // Prevent shooting if already fired

        hasFired = true;  // Mark the gun as having fired
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);  // Create the bullet
        Rigidbody rb = bullet.GetComponent<Rigidbody>();  // Get the Rigidbody component of the bullet

        rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);  // Fire in the forward direction
    }

    // Enemy shoot method (fires in a specific direction)
    public void EnemyShoot(Vector3 direction)
    {
        if (hasFired) return;  // Prevent shooting if already fired

        hasFired = true;  // Mark the gun as having fired
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);  // Create the bullet
        Rigidbody rb = bullet.GetComponent<Rigidbody>();  // Get the Rigidbody component of the bullet

        // Fire in the provided direction
        rb.AddForce(direction * bulletForce, ForceMode.Impulse);  // Launch the bullet
    }

    // Function to handle shooting the gun
   
}

