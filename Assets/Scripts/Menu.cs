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
    }

    public void QuitGame()
    {
        print("Exit");
        Application.Quit();
    }
}
