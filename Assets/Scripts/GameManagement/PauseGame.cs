using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    bool gamePaused = false;
    public Canvas pauseMenuUI;
    public Canvas playerHUD;
    public Canvas upgradeUI;
    public Canvas deathUI;

    void Start()
    {
        upgradeUI.enabled = false;
        pauseMenuUI.enabled = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !gamePaused)
        {
            pauseGame();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && gamePaused)
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
        upgradeUI.enabled = false;
    }

    void unpauseGame()
    {
        Time.timeScale = 1;
        gamePaused = false;
        playerHUD.enabled = true;
        pauseMenuUI.enabled= false;
        upgradeUI.enabled= false;
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void restartLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Hallway");
    }
}
