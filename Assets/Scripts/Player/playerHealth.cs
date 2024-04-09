using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class playerHealth : MonoBehaviour
{
    public int health = 5;
    public float damageCooldown = 1;
    public bool canTakeDamage = true;

    public TextMeshProUGUI healthCountText;
    void Start()
    {
        
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
    }

    IEnumerator takeDamage()
    {
        canTakeDamage = false;
        health--;
        yield return new WaitForSeconds(damageCooldown);
        canTakeDamage = true;
    }
}
