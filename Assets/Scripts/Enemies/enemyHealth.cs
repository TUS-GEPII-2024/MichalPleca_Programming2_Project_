using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealth : MonoBehaviour
{
    public int health = 5;
    public float damageCooldown = 0.25f;
    public bool canTakeDamage = true;
    public Animator enemyAnimator;

    public bool enemyDead = false;
    void Start()
    {
        canTakeDamage = true;
    }

    void Update()
    {
        if(health <= 0 || enemyDead)
        {
            enemyAnimator.SetTrigger("enemyDead");
            enemyDead = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("playerFist") || collision.gameObject.CompareTag("Player")) && canTakeDamage)
        {
            takeDamage();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.CompareTag("playerFist") || collision.gameObject.CompareTag("Bullet")) && canTakeDamage)
        {
            takeDamage();
        }
    }

    IEnumerator takeDamage()
    {
        Debug.Log("enemy took damage");
        canTakeDamage = false;
        health--;
        yield return new WaitForSeconds(damageCooldown);
        canTakeDamage = true;
    }
}
