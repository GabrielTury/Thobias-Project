using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public string nextLevel;

    public void LoadLevel()
    {
        SceneManager.LoadScene(nextLevel);
        Time.timeScale = 2.0f;
    }

    public void Resume()
    {
        Time.timeScale = 2.0f;
    }

    public void QuitGame()
    {
        print("Exit");
        Application.Quit();
        Time.timeScale = 2.0f;
    }
}
