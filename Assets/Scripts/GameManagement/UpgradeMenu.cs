using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    bool gamePaused = false;
    public Canvas pauseMenuUI;
    public Canvas playerHUD;
    public Canvas upgradeUI;

    public playerHealth playerHealth;
    public playerAttack playerAttack;
    public PlayerMovementController playerMovementController;
    
    public playerCollectibles playerCollectibles;

    void Start()
    {
        pauseMenuUI.enabled = false;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) & !gamePaused)
        {
            pauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab) & gamePaused)
        {
            unpauseGame();
        }
    }

    void pauseGame()
    {
        Time.timeScale = 0;
        gamePaused = true;
        upgradeUI.enabled = true;
        pauseMenuUI.enabled = false;
        playerHUD.enabled = false;
    }

    void unpauseGame()
    {
        Time.timeScale = 1;
        gamePaused = false;
        playerHUD.enabled = true;
        pauseMenuUI.enabled = false;
        upgradeUI.enabled = false;
    }

    public void maxHealthUpgrade()
    {
        if (playerCollectibles.boneCount >= 2)
        {
            playerHealth.maxHealth++;
            playerCollectibles.boneCount -= 2;
            Debug.Log("max Health Upgrade Got");
        }
    }
}