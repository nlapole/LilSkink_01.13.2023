using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this doesn't quite work
//hold onto for reference
public class DynamicWaveAudio : MonoBehaviour
{
    public Transform player;
    public AudioSource audioSource;
    public float maxVolume = 1.0f;
    
    void Update() {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        float volume = 1.0f - distance / audioSource.maxDistance;
        audioSource.volume = Mathf.Clamp(volume, 0.0f, maxVolume);
    }
}
