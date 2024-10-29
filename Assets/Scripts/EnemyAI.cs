using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public float walkspeed;
    public float turnSpeed = 5f;     // How fast the enemy turns
    public float accuracyOffset = 1.5f;  // How much error is added to the enemy's aim
    public AudioSource audioSource;
    private Vector3 _startPosition;   
    private bool _walkingAway = true; 
    private bool _hasShot = false;    
    public Transform player;          
    
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        
        // Start walking directly
        _walkingAway = true;
        animator.SetBool("isWalking", true);
    }

    private void Update()
    {
        // If still walking and not yet reached walk distance
        if (_walkingAway)
        {
            transform.Translate(Vector3.forward * walkspeed * Time.deltaTime);  // Move forward
            // Adjust pitch based on walk speed
            audioSource.pitch = walkspeed / 2.0f;  // Adjust this divisor to control sensitivity
        }
        else
        {
            audioSource.pitch = 1.0f;  // Reset to normal pitch when not walking
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

