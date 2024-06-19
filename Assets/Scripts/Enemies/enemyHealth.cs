using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class enemyHealth : MonoBehaviour
{
    public int health = 5;
    private int healthStored;
    public float destroyDelay = 2;
    public Vector2 hurtRecoil;

    public bool enemyDead = false;

    public GameObject deadCollider;
    public AudioSource audioSource;

    private Animator enemyAnimator;
    private Rigidbody2D enemyRB;
    private CapsuleCollider2D capsuleCollider;
    private ShadowCaster2D shadowCaster;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        healthStored = health;
        enemyAnimator = GetComponent<Animator>();
        enemyRB = GetComponent<Rigidbody2D>();
        shadowCaster = GetComponent<ShadowCaster2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        //boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if(healthStored > health)
        {
            healthStored = health;
            damageKnockback();
        }

        if (health <= 0 && !enemyDead)
        {
            StartCoroutine(enemyDeath());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            health -= playerAttack.instance.rangedDamage;
            Destroy(collision.gameObject);
        }
    }

    void damageKnockback()
    {
        Vector2 recoilForce;
        recoilForce.x = -transform.localScale.x * hurtRecoil.x;
        recoilForce.y = hurtRecoil.y;
        enemyRB.AddForce(recoilForce, ForceMode2D.Impulse);
    }

        IEnumerator enemyDeath()
    {
        enemyDead = true;
        audioSource.enabled = false;
        if (shadowCaster != null)
        {
            shadowCaster.enabled = false;
        }
        capsuleCollider.enabled = false;
        //boxCollider.enabled = false;
        deadCollider.SetActive(true);
        enemyAnimator.SetTrigger("enemyDead");

        yield return new WaitForSeconds(destroyDelay);

        Destroy(gameObject);
    }
}
