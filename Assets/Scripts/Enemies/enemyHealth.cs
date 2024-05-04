using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class enemyHealth : MonoBehaviour
{
    public int health = 5;
    public float destroyDelay = 2;
    
    public bool enemyDead = false;

    public GameObject deadCollider;
    public AudioSource audioSource;

    private Animator enemyAnimator;
    //private BoxCollider2D boxCollider;
    private CapsuleCollider2D capsuleCollider;
    private ShadowCaster2D shadowCaster;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        shadowCaster = GetComponent<ShadowCaster2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        //boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (health <= 0 && !enemyDead)
        {
            StartCoroutine(enemyDeath());
        }
    }
        IEnumerator enemyDeath()
    {
        enemyDead = true;
        audioSource.enabled = false;
        shadowCaster.enabled = false;
        capsuleCollider.enabled = false;
        //boxCollider.enabled = false;
        deadCollider.SetActive(true);
        enemyAnimator.SetTrigger("enemyDead");

        yield return new WaitForSeconds(destroyDelay);

        Destroy(gameObject);
    }
}
