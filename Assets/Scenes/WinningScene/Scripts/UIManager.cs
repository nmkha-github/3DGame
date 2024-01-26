using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{   
    public AudioSource audioSource;
    public AudioClip celebrateAudio;
    public AudioClip startAudio;
    public float fireworksSoundStartWait;
    public float fireworksDelay;
    public AudioClip fireworksAudio;
    float volume;
    private void Start()
    {
        volume = PlayerPrefs.GetFloat("Volume", 1f);
        audioSource.PlayOneShot(celebrateAudio, volume);
        Invoke(nameof(PlayStartSound), 1f);
        InvokeRepeating(nameof(PlaySound), fireworksSoundStartWait, fireworksDelay);
    }
    void PlaySound()
    {
        audioSource.PlayOneShot(fireworksAudio, volume);
    }
    void PlayStartSound()
    {
        audioSource.PlayOneShot(startAudio, volume);
    }
    public void restartButton()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("GamePlay");
    }
    public void returnButton()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }
}
