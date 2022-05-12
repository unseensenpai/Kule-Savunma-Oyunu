using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad = "MainLevel";
    public bool gameRestarted = false;
    public SceneFader sceneFader;

    public void Play()
    {
        if (gameRestarted==true)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1f;
        }
        else
        {
            // sceneFader.FadeTo(levelToLoad);
            SceneManager.LoadScene(levelToLoad);
            gameRestarted = true;
        }            
    }

    public void Quit()
    {
        Debug.Log("Çýkýþ yapýlýyor...");
        Application.Quit();
    }

}
