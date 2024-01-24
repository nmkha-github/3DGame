using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void restartButton()
    {
        SceneManager.LoadScene("GamePlay");
    }
    public void returnButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
