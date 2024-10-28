using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public float walkDistance = 5f;  // Distance to walk before turning
    public float turnSpeed = 5f;     // How fast the enemy turns
    public float accuracyOffset = 1.5f;  // How much error is added to the enemy's aim

    private Vector3 _startPosition;   // Store the start position to calculate distance walked
    private bool _walkingAway = true; // Is the enemy still walking away from the player?
    private bool _hasShot = false;    // To prevent the enemy from shooting more than once
    public Transform player;          // Reference to the player's position

    public UnityEvent onSignalEvent;  // Triggered when the sound signal goes off

    private void Start()
    {
        _startPosition = transform.position;
        // Start walking directly
        _walkingAway = true;
    }

    private void Update()
    {
        // If still walking and not yet reached walk distance
        if (_walkingAway && Vector3.Distance(_startPosition, transform.position) < walkDistance)
        {
            transform.Translate(Vector3.forward * Time.deltaTime);  // Move forward
        }
    }

    // Triggered by the sound signal event
    public void TurnAndShoot()
    {
        if (_walkingAway)
        {
            _walkingAway = false;  // Stop walking
            if (!_hasShot)
            {
                StartCoroutine(TurnAndShootCoroutine());
            }
        }
    }

    // Coroutine to smoothly turn and shoot after signal
    private IEnumerator TurnAndShootCoroutine()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);

        // Smoothly turn towards the player
        while (Quaternion.Angle(transform.rotation, lookRotation) > 0.1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
            yield return null;
        }

        // Once turned, shoot at the player with some inaccuracy
        ShootWithError();
    }

    private void ShootWithError() 
    {
        Vector3 aimError = new Vector3(Random.Range(-accuracyOffset, accuracyOffset), Random.Range(-accuracyOffset, accuracyOffset), 0);
        Vector3 fireDirection = (player.position + aimError - transform.position).normalized;

        GetComponent<Gun>().EnemyShoot(fireDirection);

        _hasShot = true; 
    }
}

