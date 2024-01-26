using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueButton : MonoBehaviour
{
    public GameObject PauseGamePanel;


    public void ContinueGame()
    {
        PauseGamePanel.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
