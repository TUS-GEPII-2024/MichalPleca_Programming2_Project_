using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rabbitController : MonoBehaviour
{
    [SerializeField]
    private bool playerDetected = false;
    private Transform rabbitTransform;
    public Animator rabbitAnimator;
    public CircleCollider2D playerDetectionCollider;

    public float enemySpeed = 1f;
    void Start()
    {
        rabbitTransform = transform.parent;
    }
    void Update()
    {
        if (playerDetected)
        {
            Vector3 directionToPlayer = (PlayerMovementController.instance.transform.position - rabbitTransform.position).normalized;
            rabbitTransform.position += directionToPlayer * enemySpeed * Time.deltaTime;

            if (directionToPlayer.x < 0)
            {
                rabbitTransform.localScale = new Vector3(-1, 1, 1);
            }
            else if (directionToPlayer.x > 0)
            {
                rabbitTransform.localScale = new Vector3(1, 1, 1);
            }

            rabbitAnimator.SetBool("playerDetected", true);
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