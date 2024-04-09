using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealth : MonoBehaviour
{
    public int health = 5;
    public float damageCooldown = 1;
    public bool canTakeDamage = true;
    void Start()
    {

    }

    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DamageDealer") && canTakeDamage)
        {
            takeDamage();
        }
    }

    IEnumerator takeDamage()
    {
        canTakeDamage = false;
        health--;
        yield return new WaitForSeconds(damageCooldown);
        canTakeDamage = true;
    }
}
