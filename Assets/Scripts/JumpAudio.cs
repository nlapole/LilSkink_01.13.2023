using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAudio : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip[] jumpAudio;    

    void Start() 
    {
        audioSource = gameObject.AddComponent<AudioSource>(); // Create a new audio source
    }
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            audioSource.clip = jumpAudio[Random.Range(0, jumpAudio.Length)];
            audioSource.Play();
        }         
    }
}

