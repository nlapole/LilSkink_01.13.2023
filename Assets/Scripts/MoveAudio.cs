
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAudio : MonoBehaviour
{
     Animator myAnimator;
     AudioSource audioSource;
     public AudioClip moveAudio;
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        audioSource = gameObject.AddComponent<AudioSource>(); // Create a new audio source
    }    
    void Update()
    {
        bool isRunning = myAnimator.GetBool("isRunning");
        if (isRunning && !audioSource.isPlaying) {
            audioSource.clip = moveAudio;
            audioSource.Play();
        } else if (!isRunning && audioSource.isPlaying) {
            audioSource.Stop();
        }
    }        
}


