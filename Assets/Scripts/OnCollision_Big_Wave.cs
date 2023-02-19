using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollision_Big_Wave : MonoBehaviour

{
    public AudioSource audioSource;
    public AudioClip deadSkinkAudio;

    void OnCollisionEnter2D(Collision2D other) 
    {
            audioSource.clip = deadSkinkAudio;
            audioSource.Play();
    }
}

