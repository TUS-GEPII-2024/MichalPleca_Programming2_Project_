using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPatrol : MonoBehaviour
{
    public float patrolSpeed = 3;
    public float chaseSpeed = 4;

    [SerializeField]
    private bool playerDetected = false;
    public Transform enemyTransform;
    public enemyHealth enemyHealth;
    private CircleCollider2D playerDetectionCollider;

    public Transform patrolPoint1;
    public Transform patrolPoint2;
    private Transform currentPatrolPoint;
    void Start()
    {
        currentPatrolPoint = patrolPoint1;
        playerDetectionCollider = GetComponent<CircleCollider2D>();
    }
    void Update()
    {
        if (playerDetected)
        {
            navigationToPlayer();
        }
        else
        {
            navigationToPoints();
        }

        if (enemyHealth.enemyDead == true)
        {
            playerDetected = false;
        }
    }

    private void navigationToPlayer()
    {
        Vector3 directionToPlayer = (PlayerMovementController.instance.transform.position - enemyTransform.position).normalized;
        enemyTransform.position += directionToPlayer * chaseSpeed * Time.deltaTime;

        if (directionToPlayer.x < 0)
        {
            enemyTransform.localScale = new Vector3(-1, 1, 1);
        }
        else if (directionToPlayer.x > 0)
        {
            enemyTransform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void navigationToPoints()
    {
        Vector3 directionToPoint = (currentPatrolPoint.position - enemyTransform.position).normalized;
        enemyTransform.position += directionToPoint * patrolSpeed * Time.deltaTime;

        if (enemyTransform.position.x >= patrolPoint1.position.x)
        {
            currentPatrolPoint = patrolPoint2;
        }
        else if (enemyTransform.position.x <= patrolPoint2.position.x)
        {
            currentPatrolPoint = patrolPoint1;
        }

        if (directionToPoint.x < 0)
        {
            enemyTransform.localScale = new Vector3(-1, 1, 1);
        }
        else if (directionToPoint.x > 0)
        {
            enemyTransform.localScale = new Vector3(1, 1, 1);
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
