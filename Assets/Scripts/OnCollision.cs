using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollision : MonoBehaviour

{
    public AudioSource audioSource;
    public AudioClip[] bounceAudio;

    void OnCollisionEnter2D(Collision2D other) 
    {
            audioSource.clip = bounceAudio[Random.Range(0, bounceAudio.Length)];
            audioSource.Play();
    }
}
