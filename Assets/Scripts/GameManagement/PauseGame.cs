using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    bool gamePaused = false;
    public Canvas pauseMenuUI;
    public Canvas playerHUD;

    void Start()
    {
        pauseMenuUI.enabled = false;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) & !gamePaused)
        {
            pauseGame();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) & gamePaused)
        {
            unpauseGame();
        }
    }

    void pauseGame()
    {
        Time.timeScale = 0;
        gamePaused = true;
        pauseMenuUI.enabled = true;
        playerHUD.enabled = false;
    }

    void unpauseGame()
    {
        Time.timeScale = 1;
        gamePaused = false;
        playerHUD.enabled = true;
        pauseMenuUI.enabled= false;
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
