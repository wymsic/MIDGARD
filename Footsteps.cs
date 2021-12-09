using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    PlayerLocomotion playerLocomotion;
    PlayerManager playerManager;

    [SerializeField]
    private AudioClip[] clips;

    private AudioSource audioSource;


    //This script simply creates a array of sound clips that randomyl play when the step function is triggered. Used by animation events to trigger walking SFX


    private void Awake()
    {
        playerLocomotion = GetComponent<PlayerLocomotion>();
        playerManager = GetComponent<PlayerManager>();

        audioSource = GetComponent<AudioSource>();
    }

    private void Step()
    {
        if (playerLocomotion.isGrounded && !playerManager.isInteracting)
        {
            AudioClip clip = GetRandomClip();
            audioSource.PlayOneShot(clip);
        }
    }

    private void Jumpstep()
    {
        if (playerLocomotion.isGrounded)
        {
            AudioClip clip = GetRandomClip();
            audioSource.PlayOneShot(clip);
        }
    }


    private AudioClip GetRandomClip()
    {
        return clips[UnityEngine.Random.Range(0, clips.Length)];
    }
}
