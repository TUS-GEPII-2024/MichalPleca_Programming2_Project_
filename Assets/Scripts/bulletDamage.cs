using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletDamage : MonoBehaviour
{
    public int damage = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("trigger");
        collision.gameObject.GetComponent<enemyHealth>().health -= damage;
    }
}
