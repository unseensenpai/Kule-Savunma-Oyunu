using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad = "LevelSelect";
    public bool gameRestarted = false;
    public SceneFader sceneFader;

    public void Play()
    {
        if (gameRestarted==true)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1f;
            PlayerStats.Lives = 10;
            PlayerStats.Money = 1000;
        }
        else
        {
            // sceneFader.FadeTo(levelToLoad);
            SceneManager.LoadScene(levelToLoad);
            gameRestarted = true;
            PlayerStats.Lives = PlayerStats.prevLives;
            PlayerStats.Money = PlayerStats.prevMoney;
        }            
    }

    public void Quit()
    {
        Debug.Log("Çýkýþ yapýlýyor...");
        Application.Quit();
    }

}
