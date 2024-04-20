using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class playerHealth : MonoBehaviour
{
    public int health = 4;
    public int maxHealth = 5;
    [HideInInspector]
    public int maxHealthStored = 5;

    public float damageCooldown = 1;
    public bool canTakeDamage = true;

    public float megaDamageCooldown = 0.75f;
    public bool canTakeMegaDamage = true;

    public float healingCooldown = 1;

    public AudioClip boozeGulpClip;
    public AudioSource characterInteractionSFX;

    public TextMeshProUGUI healthCountText;
    void Start()
    {
        maxHealthStored = maxHealth;
    }

    void Update()
    {
        healthCountText.text = health.ToString();
    }
    private void OnTriggerEnter2D(Collider2D collision)
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
        yield return new WaitForSeconds(damageCooldown);
        canTakeDamage = true;
    }

    IEnumerator takeMegaDamage()
    {
        canTakeMegaDamage = false;
        health -= 2;
        yield return new WaitForSeconds(megaDamageCooldown);
        canTakeMegaDamage = true;
    }
}
