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

    public GameObject rangedPrefab;
    public bool rangedOnCooldown = false;
    public float rangedDestroyDelay = 1f;
    public float rangedProjectileForce = 5f;

    private bool facingRight;
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

            if (directionToPlayer.x < 0)
            {
                enemyTransform.localScale = new Vector3(-1, 1, 1);

                facingRight = true;

                Vector3 rangedScale = rangedPrefab.transform.localScale;
                rangedScale.x = -1;
                rangedPrefab.transform.localScale = rangedScale;
            }
            else if (directionToPlayer.x > 0)
            {
                enemyTransform.localScale = new Vector3(1, 1, 1);

                facingRight = false;

                Vector3 rangedScale = rangedPrefab.transform.localScale;
                rangedScale.x = 1;
                rangedPrefab.transform.localScale = rangedScale;
            }
            enemyAnimator.SetTrigger("plantAlerted");

            if (!rangedOnCooldown)
            {
                rangedAttack();
            }
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

    IEnumerator rangedAttack()
    {
        rangedOnCooldown = true;
        GameObject rangedProjectile = Instantiate(rangedPrefab, transform.position, transform.rotation);
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
        yield return new WaitForSeconds(rangedDestroyDelay);
        rangedOnCooldown = false;
    }
}
