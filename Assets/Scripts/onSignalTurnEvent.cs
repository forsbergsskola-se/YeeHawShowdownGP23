using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnSignalTurnEvent : MonoBehaviour
{
    public AudioClip signalSound;
    private AudioSource audioSource;

    public UnityEvent onSignalTriggerd;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(triggerSignalRandomDelay());
    }

    // Update is called once per frame
    private IEnumerator triggerSignalRandomDelay()
    {
        while (true)
        {
            float delay = Random.Range(3f, 5f);

            if (signalSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(signalSound);
            }
        }
    }
}
