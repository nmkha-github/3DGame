using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSoundParent : MonoBehaviour
{
    AudioSource sound;
    private void Awake()
    {
        sound = GetComponent<AudioSource>();
        if (sound != null)
        {
            sound.volume = PlayerPrefs.GetFloat("Volume", 1f);
        }

    }
    private void Start()
    {   if (transform.parent != null)
            transform.SetParent(transform.parent.parent);

        if (sound != null)
            sound.PlayOneShot(sound.clip);
    }
}
