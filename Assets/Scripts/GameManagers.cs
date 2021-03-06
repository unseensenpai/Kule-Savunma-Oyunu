using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagers : MonoBehaviour
{

    public static bool GameIsOver;
    public GameObject gameOverUI;
    public GameObject completeLevelUI;


    void Start()
    {
        GameIsOver = false;
    }
    private void Awake()
    {
        completeLevelUI.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (GameIsOver)
        {
            return;            
        }

        if(PlayerStats.Lives <= 0)
        {
            EndGame();
        }
    }
    public void EndGame()
    {
        gameOverUI.SetActive(true);
        GameIsOver = true;        
    }

    public void WinLevel()
    {   
        completeLevelUI.SetActive(true);
        GameIsOver = true;
    }
}
