using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSoundParent : MonoBehaviour
{
    private void Start()
    {
        if (transform.parent != null)
        transform.SetParent(transform.parent.parent);

        AudioSource sound = GetComponent<AudioSource>();
        if (sound != null)
        {
            sound.volume = PlayerPrefs.GetFloat("Volume", 1f);
        }
    }
}
