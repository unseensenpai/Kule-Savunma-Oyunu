using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text roundsText;
    public string returnToMenu = "MainMenu";
    public GameObject goui;
    public SceneFader sceneFader;
    public string menuSceneName = "MainMenu";


    private void OnEnable()
    {
        roundsText.text = PlayerStats.Rounds.ToString();
    }

    public void Retry()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {        
        Debug.Log("Men�ye d�n�l�yor..");
        SceneManager.LoadScene(returnToMenu);
    }

    public void Toggle()
    {
        goui.SetActive(!goui.activeSelf);

        if (goui.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
        Debug.Log("Go to menu");
        sceneFader.FadeTo(menuSceneName);
    }
}
