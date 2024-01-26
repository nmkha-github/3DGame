using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip hoverSound;
    public AudioClip clickSound;

    private float volume;
    void Update()
    {
        volume = PlayerPrefs.GetFloat("Volume", 1f);
    }
    public void Hover()
    {
        audioSource.PlayOneShot(hoverSound, volume);
    }

    public void Click()
    {
        audioSource.PlayOneShot(clickSound, volume);
    }
}
