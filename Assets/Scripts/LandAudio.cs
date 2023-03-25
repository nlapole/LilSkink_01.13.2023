using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandAudio : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip landAudio;
    private bool isGrounded = true;
    public float volumeLand = 1.0f;

    void Start() 
    {
        audioSource = gameObject.AddComponent<AudioSource>(); // Create a new audio source        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" && !isGrounded)
        {
            AudioSource.PlayClipAtPoint(landAudio, transform.position, volumeLand);
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" && isGrounded)
        {
            isGrounded = false;
        }
    }

}
