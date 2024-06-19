using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyIdle : MonoBehaviour
{
    [SerializeField]
    private bool playerDetected = false;
    private bool spawnLight = true;
    private Transform enemyTransform;
    public Animator enemyAnimator;
    public enemyHealth enemyHealth;
    public CircleCollider2D playerDetectionCollider;
    public GameObject lightPrefab;

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

        if(enemyHealth.enemyDead == true || playerHealth.instance.dead == true)
        {
            playerDetected = false;
            if (enemyHealth.enemyDead == true && spawnLight)
            {
                StartCoroutine(enemyDead());
            }
        }
    }

    IEnumerator enemyDead()
    {
        spawnLight = false;
        GameObject fireLight = Instantiate(lightPrefab, transform.position, transform.rotation);
        yield return new WaitForSeconds(enemyHealth.destroyDelay - 1);
        Destroy(fireLight);
    }

  // private void OnCollisionEnter2D(Collision2D collision)
   // {
        //if(collision.gameObject.tag == "Player")
        //{
        //    playerDetected = true;
        //    playerDetectionCollider.enabled = false;
        //}
   // }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerDetected = true;
            playerDetectionCollider.enabled = false;
        }
    }
}
