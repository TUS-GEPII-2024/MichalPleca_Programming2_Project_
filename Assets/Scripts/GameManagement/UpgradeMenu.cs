using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    bool gamePaused = false;
    public Canvas pauseMenuUI;
    public Canvas playerHUD;
    public Canvas upgradeUI;
    public Canvas winUI;
    public Image unavailableImage;

    public playerHealth playerHealth;
    public playerAttack playerAttack;
    public PlayerMovementController playerMovementController;
    
    public playerCollectibles playerCollectibles;

    public GameObject jumpUpgrade;
    public GameObject dashUpgrade;
    public GameObject bulletDmg;

    private bool doubleJumpGot = false;
    private bool dashGot = false;
    private bool bulletDmgGot = false;

    private void Start()
    {
        winUI.enabled = false;
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
        if (playerAttack.instance.playerHasGunCollectible)
        {
            playerAttack.instance.playerHasGun = false;
        }
        Time.timeScale = 0;
        gamePaused = true;
        upgradeUI.enabled = true;
        pauseMenuUI.enabled = false;
        playerHUD.enabled = false;
    }

    void unpauseGame()
    {
        if (playerAttack.instance.playerHasGunCollectible)
        {
            playerAttack.instance.playerHasGun = true;
        }
        Time.timeScale = 1;
        gamePaused = false;
        playerHUD.enabled = true;
        pauseMenuUI.enabled = false;
        upgradeUI.enabled = false;
    }

    public void maxHealthUpgrade()
    {
        if (playerHealth.maxHealth <= playerHealth.maxHealthStored + 2)
        {
            if (playerCollectibles.boneCount >= 2 && doubleJumpGot)
            {
                playerHealth.maxHealth++;
                playerHealth.health++;
                playerCollectibles.boneCount -= 2;
                Debug.Log("max Health Upgrade Got");
            }
        }
    }

    public void walkSpeedUpgrade()
    {
        if (playerCollectibles.boneCount >= 3 && doubleJumpGot)
        {
            PlayerMovementController.instance.speed += 0.5f;
            playerCollectibles.boneCount -= 3;
            Debug.Log("walk Speed Upgrade Got");
        }
    }

    public void doubleJumpUpgrade()
    {
        if (playerCollectibles.boneCount >= 2 && !doubleJumpGot)
        {
            doubleJumpGot = true;
            unavailableImage.enabled = false;
            PlayerMovementController.instance.jumpAmount = 2;
            playerCollectibles.boneCount -= 2;
            Debug.Log("double Jump Upgrade Got");
            Destroy(jumpUpgrade);
        }
    }

    public void getDashUpgrade()
    {
        if (playerCollectibles.boneCount >= 3 && !dashGot && doubleJumpGot)
        {
            dashGot = true;
            PlayerMovementController.instance.dashEnabled = true;
            playerCollectibles.boneCount -= 3;
            Debug.Log("dash Upgrade Got");
            Destroy(dashUpgrade);
        }
    }

    public void bulletDmgUpgrade()
    {
        if (playerCollectibles.boneCount >= 5 && !bulletDmgGot && doubleJumpGot)
        {
            bulletDmgGot = true;
            playerAttack.instance.rangedDamage += 1;
            playerCollectibles.boneCount -= 5;
            Debug.Log("dmg Upgrade Got");
            Destroy(bulletDmg);
        }
    }
}