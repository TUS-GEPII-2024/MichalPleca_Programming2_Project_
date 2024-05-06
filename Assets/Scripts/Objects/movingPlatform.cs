using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform : MonoBehaviour
{
    public bool checkVertical = false;
    public bool checkHorizontal = true;
    private Transform platformTransform;
    public float moveSpeed = 1f;
 
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
        if (checkHorizontal)
        {
            if (platformTransform.position.x >= patrolPoint1.position.x)
            {
                currentPatrolPoint = patrolPoint2;
            }
            else if (checkHorizontal && (platformTransform.position.x <= patrolPoint2.position.x))
            {
                currentPatrolPoint = patrolPoint1;
            }
        }
        else if (checkVertical)
        {
            if (platformTransform.position.y >= patrolPoint1.position.y)
            {
                currentPatrolPoint = patrolPoint2;
            }
            else if (platformTransform.position.y <= patrolPoint2.position.y)
            {
                currentPatrolPoint = patrolPoint1;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(null);
        }
    }
}
