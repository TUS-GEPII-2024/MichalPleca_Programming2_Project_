using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DamageDealer"))
        {
            collision.gameObject.GetComponent<enemyHealth>().health -= playerAttack.instance.rangedDamage;
            Destroy(gameObject);
        }
    }
}
