using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip hoverSound;
    public AudioClip clickSound;

    private float volume;
    private void Start()
    {
        volume = PlayerPrefs.GetFloat("Volume", volume);
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
