using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootstepSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip stepAudio;
    public AudioClip runningAudio; 

    private void Start()
    {
        audioSource.volume = PlayerPrefs.GetFloat("Volume", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                audioSource.enabled = true;
                audioSource.clip = runningAudio;
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            } else 
            {
                audioSource.enabled = true;
                audioSource.clip = stepAudio;
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }
            
        } else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            audioSource.enabled = true;
            audioSource.clip = stepAudio;
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.clip = stepAudio;
            audioSource.enabled = false;
        }
    }
}
