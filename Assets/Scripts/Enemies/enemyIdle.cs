using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyIdle : MonoBehaviour
{
    [SerializeField]
    private bool playerDetected = false;
    private Transform enemyTransform;

    public float enemySpeed = 1f;
    void Start()
    {
        enemyTransform = transform.parent;
    }
    void Update()
    {
        if (playerDetected)
        {
            Vector3 directionToPlayer = (PlayerMovementController.instance.transform.position - enemyTransform.position).normalized;
            enemyTransform.position += directionToPlayer * enemySpeed * Time.deltaTime;

            if (directionToPlayer.x < 0)
            {
                enemyTransform.localScale = new Vector3(-1, 1, 1);
            }
            else if (directionToPlayer.x > 0)
            {
                enemyTransform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerDetected = true;
        }
    }
}
