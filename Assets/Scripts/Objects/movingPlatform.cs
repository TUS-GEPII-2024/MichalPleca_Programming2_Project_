using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform : MonoBehaviour
{
    private Transform platformTransform;
    public float moveSpeed;
 
    public Transform patrolPoint1;
    public Transform patrolPoint2;
    private Transform currentPatrolPoint;

    void Start()
    {
        currentPatrolPoint = patrolPoint1;
        platformTransform = transform;
    }

    void Update()
    {
        navigationToPoints();
    }

    private void navigationToPoints()
    {
        Vector3 directionToPoint = (currentPatrolPoint.position - platformTransform.position).normalized;
        platformTransform.position += directionToPoint * moveSpeed * Time.deltaTime;

        if (platformTransform.position.x >= patrolPoint1.position.x)
        {
            currentPatrolPoint = patrolPoint2;
        }
        else if (platformTransform.position.x <= patrolPoint2.position.x)
        {
            currentPatrolPoint = patrolPoint1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}
