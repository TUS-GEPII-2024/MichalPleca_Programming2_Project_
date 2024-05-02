using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletDamage : MonoBehaviour
{
    public int damage = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DamageDealer"))
        {
            collision.gameObject.GetComponent<enemyHealth>().health -= damage;
        }
    }
}
