using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class enemyRanged : MonoBehaviour
{
    [SerializeField]
    private bool playerDetected = false;
    private Transform enemyTransform;
    public Animator enemyAnimator;
    public enemyHealth enemyHealth;
    public Light2D enemyLight;
    private CircleCollider2D playerDetectionCollider;
    private AudioSource enemyAudio;

    public GameObject rangedPrefab;
    public bool rangedOnCooldown = false;
    public float rangedCooldownTime = 1.5f;
    public float rangedDestroyDelay = 1f;
    public float rangedProjectileForce = 5f;

    private bool facingRight;
    void Start()
    {
        enemyAudio = GetComponent<AudioSource>();
        playerDetectionCollider = GetComponent<CircleCollider2D>();
        enemyTransform = transform.parent;
    }
    void Update()
    {
        if (playerDetected)
        {
            Vector3 directionToPlayer = (PlayerMovementController.instance.transform.position - enemyTransform.position).normalized;

            if (directionToPlayer.x > 0)
            {
                enemyTransform.localScale = new Vector3(-1, 1, 1);

                facingRight = true;

                Vector3 rangedScale = rangedPrefab.transform.localScale;
                rangedScale.x = -1;
                rangedPrefab.transform.localScale = rangedScale;
            }
            else if (directionToPlayer.x < 0)
            {
                enemyTransform.localScale = new Vector3(1, 1, 1);

                facingRight = false;

                Vector3 rangedScale = rangedPrefab.transform.localScale;
                rangedScale.x = 1;
                rangedPrefab.transform.localScale = rangedScale;
            }

            if (!rangedOnCooldown)
            {
                StartCoroutine(rangedAttack());
            }
        }

        if (enemyHealth.enemyDead == true)
        {
            playerDetected = false;
            enemyLight.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && enemyHealth.enemyDead == !true)
        {
            playerDetected = true;
            enemyAnimator.SetBool("plantAlerted", true);
            //playerDetectionCollider.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerDetected = false;
            enemyAnimator.SetBool("plantAlerted", false);
        }
    }

    IEnumerator rangedAttack()
    {
        //enemy ranged attack
        rangedOnCooldown = true;
        enemyAudio.Play();
        GameObject rangedProjectile = Instantiate(rangedPrefab, enemyTransform.position, enemyTransform.rotation);
        Rigidbody2D rangedProjectileRB = rangedProjectile.GetComponent<Rigidbody2D>();
        if (facingRight)
        {
            rangedProjectileRB.AddForce(Vector2.right * rangedProjectileForce, ForceMode2D.Impulse);
        }
        else
        {
            rangedProjectileRB.AddForce(Vector2.left * rangedProjectileForce, ForceMode2D.Impulse);
        }
        Destroy(rangedProjectile, rangedDestroyDelay);
        yield return new WaitForSeconds(rangedCooldownTime);
        rangedOnCooldown = false;
    }
}
