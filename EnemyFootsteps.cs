using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This script simply creates a array of sound clips that randomyl play when the step function is triggered by Enemy. Used by animation events to trigger walking SFX

public class EnemyFootsteps : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] clips;

    private AudioSource audioSource;



    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Step()
    {
            AudioClip clip = GetRandomClip();
            audioSource.PlayOneShot(clip);
    }

    private AudioClip GetRandomClip()
    {
        return clips[UnityEngine.Random.Range(0, clips.Length)];
    }
}
