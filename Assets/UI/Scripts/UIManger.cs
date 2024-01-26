using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManger : MonoBehaviour
{
    public Slider volumeSlider;

    private const string VolumeKey = "Volume";

    // Start is called before the first frame update
    void Start()
    {
        // Set the initial volume based on the saved value or default to 1.0f
        float savedVolume = PlayerPrefs.GetFloat(VolumeKey, 1.0f);
        if (volumeSlider)
        {
            volumeSlider.value = savedVolume;
            SetVolume(savedVolume);

            // Add a listener to the slider to detect changes in its value
            volumeSlider.onValueChanged.AddListener(delegate { OnVolumeChanged(); });
        }
    }

    // Function to be called when the slider value changes
    void OnVolumeChanged()
    {
        // Update the volume based on the slider value
        float newVolume = volumeSlider.value;
        SetVolume(newVolume);

        // Save the new volume setting
        PlayerPrefs.SetFloat(VolumeKey, newVolume);
        PlayerPrefs.Save();
    }

    // Function to set the volume
    void SetVolume(float volume)
    {
        // Ensure the volume is within the valid range (0 to 1)
        volume = Mathf.Clamp01(volume);

        // Implement your logic here to use the volume setting as needed
        // For example, you might adjust the volume of all audio sources in the scene
        // or communicate with other components as needed
        Debug.Log("Volume set to: " + volume);
    }

    public void startGame()
    {
        SceneManager.LoadScene("GamePlay");
    }
    public void settingScene()
    {
        SceneManager.LoadScene("Setting");
    }

    public void homeScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
