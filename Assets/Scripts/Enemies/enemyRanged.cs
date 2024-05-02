using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyRanged : MonoBehaviour
{
    [SerializeField]
    private bool playerDetected = false;
    private Transform enemyTransform;
    public Animator enemyAnimator;
    public enemyHealth enemyHealth;
    public CircleCollider2D playerDetectionCollider;

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

            enemyAnimator.SetBool("playerDetected", true);
        }

        if (enemyHealth.enemyDead == true)
        {
            playerDetected = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerDetected = true;
            playerDetectionCollider.enabled = false;
        }
    }
}