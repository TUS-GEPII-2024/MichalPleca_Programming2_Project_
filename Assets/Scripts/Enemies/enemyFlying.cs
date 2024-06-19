using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class enemyFlying : MonoBehaviour
{
    private bool playerDetected = false;
    private CircleCollider2D playerDetectionCollider;
    public enemyHealth enemyHealth;
    private Transform enemyTransform;
    private AudioSource enemyAudio;

    public float speed = 5;

    public GameObject rangedPrefab;
    public bool rangedOnCooldown = false;
    public float rangedCooldownTime = 1.5f;
    public float rangedDestroyDelay = 1f;
    public float rangedProjectileForce = 5f;

    void Start()
    {
        playerDetectionCollider = GetComponent<CircleCollider2D>();
        enemyAudio = GetComponent<AudioSource>();
        enemyTransform = transform.parent;
    }

    void Update()
    {
     if(playerDetected && !rangedOnCooldown)
        {
            StartCoroutine(rangedAttack());
        }
        else if (!playerDetected)
        {
            goTowardsPlayer();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && enemyHealth.enemyDead == !true)
        {
            playerDetected = true;
            Debug.Log("Player Detected On Trigger Enter");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerDetected = false;
        }
    }

    void goTowardsPlayer()
    {
        Vector2 directionToPlayer = (playerAttack.instance.parentTransform.position - enemyTransform.position).normalized;
        enemyTransform.position = Vector2.MoveTowards(enemyTransform.position, playerAttack.instance.parentTransform.position, speed * Time.deltaTime);
    }

    IEnumerator rangedAttack()
    {
        rangedOnCooldown = true;

        enemyAudio.Play();

        Vector2 directionToPlayer = (playerAttack.instance.parentTransform.position - transform.position).normalized;
        GameObject rangedProjectile = Instantiate(rangedPrefab, enemyTransform.position, enemyTransform.rotation);
        Rigidbody2D rangedProjectileRB = rangedProjectile.GetComponent<Rigidbody2D>();

        rangedProjectileRB.AddForce(directionToPlayer * rangedProjectileForce, ForceMode2D.Impulse);

        Destroy(rangedProjectile, rangedDestroyDelay);

        yield return new WaitForSeconds(rangedCooldownTime);

        rangedOnCooldown = false;
    }
}
