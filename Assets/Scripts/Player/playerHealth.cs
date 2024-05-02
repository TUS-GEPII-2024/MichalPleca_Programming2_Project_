using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class playerHealth : MonoBehaviour
{
    public int health = 4;
    public int maxHealth = 5;
    [HideInInspector] public int maxHealthStored = 5;

    public float damageCooldown = 1;
    public bool canTakeDamage = true;

    public float megaDamageCooldown = 0.75f;
    public bool canTakeMegaDamage = true;

    public Vector2 hurtRecoil;

    public float healingCooldown = 1;

    public bool dead = false;

    public AudioClip boozeGulpClip;
    public AudioSource characterInteractionSFX;

    public TextMeshProUGUI healthCountText;
    public Canvas playerUI;
    public Canvas deathScreen;
    void Start()
    {
        deathScreen.enabled = false;
        maxHealthStored = maxHealth;
    }

    void Update()
    {
        healthCountText.text = health.ToString();
        death();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!dead)
        {
            if (collision.gameObject.CompareTag("DamageDealer") && canTakeDamage)
            {
                StartCoroutine(takeDamage());
            }
            else if (collision.gameObject.CompareTag("MegaDamageDealer") && canTakeMegaDamage)
            {
                StartCoroutine(takeMegaDamage());
            }

            if (collision.gameObject.CompareTag("HealingItem") && health < maxHealth)
            {
                Destroy(collision.gameObject);
                StartCoroutine(healing());
            }
        }
    }

    private void death()
    {
        if (health <= 0)
        {
            PlayerMovementController.instance.playerAnimator.SetTrigger("characterDeath");
            DamageRecoil();
            PlayerMovementController.instance.speed = 0;
            PlayerMovementController.instance.playerRB.velocity = Vector3.zero;
            PlayerMovementController.instance.walkParticles.Stop();
            PlayerMovementController.instance.enabled = false;
            playerAttack.instance.enabled = false;
            playerUI.enabled = false;
            deathScreen.enabled = true;
            dead = true;
        }
    }

    IEnumerator healing()
    {
        canTakeDamage = false;
        characterInteractionSFX.clip = boozeGulpClip;
        characterInteractionSFX.Play();
        health++;
        yield return new WaitForSeconds(healingCooldown);
        canTakeDamage = true;
    }

    IEnumerator takeDamage()
    {
        canTakeDamage = false;
        health--;

        PlayerMovementController.instance.enabled = false;
        DamageRecoil();

        yield return new WaitForSeconds(0.5f);

        PlayerMovementController.instance.enabled = true;

        yield return new WaitForSeconds(damageCooldown);
        canTakeDamage = true;
    }

    IEnumerator takeMegaDamage()
    {
        canTakeMegaDamage = false;
        health -= 2;

        PlayerMovementController.instance.enabled = false;
        DamageRecoil();

        yield return new WaitForSeconds(0.75f);

        PlayerMovementController.instance.enabled = true;

        yield return new WaitForSeconds(megaDamageCooldown);
        canTakeMegaDamage = true;
    }

    void DamageRecoil()
    {
        Vector2 recoilForce;
        recoilForce.x = -transform.localScale.x * hurtRecoil.x;
        recoilForce.y = hurtRecoil.y;
        PlayerMovementController.instance.playerRB.AddForce(recoilForce, ForceMode2D.Impulse);
    }
}
