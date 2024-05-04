using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    public static playerAttack instance;

    public GameObject meleePrefab;
    public GameObject rangedPrefab;

    public float meleeDestroyDelay = 1;
    public float rangedDestroyDelay = 2;

    public float rangedProjectileForce = 1;

    public float meleeCooldownTime = 0.75f;
    public float rangedCooldownTime = 0.75f;

    public bool playerHasGun = false;

    private bool meleeOnCooldown;
    private bool rangedOnCooldown;
    private bool facingRight = true;
    private AudioSource gunshotAudio;

    private void Start()
    {
        gunshotAudio = GetComponent<AudioSource>();
        instance = this;
    }
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        if (playerHasGun)
        {
            if (Input.GetKeyDown(KeyCode.E) && !meleeOnCooldown || Input.GetKeyDown(KeyCode.RightShift) && !meleeOnCooldown)
            {
                StartCoroutine(meleeAttack());
            }
            if (Input.GetKey(KeyCode.Mouse0) && !rangedOnCooldown || Input.GetKey(KeyCode.RightControl) && !rangedOnCooldown)
            {
                StartCoroutine(rangedAttack());
            }


            if (horizontalInput < 0)
            {
                facingRight = false;

                Vector3 rangedScale = rangedPrefab.transform.localScale;
                rangedScale.x = -1;
                rangedPrefab.transform.localScale = rangedScale;

                Vector3 meleeScale = meleePrefab.transform.localScale;
                meleeScale.x = -1;
                meleePrefab.transform.localScale = meleeScale;
            }
            else if (horizontalInput > 0)
            {
                facingRight = true;

                Vector3 rangedScale = rangedPrefab.transform.localScale;
                rangedScale.x = 1;
                rangedPrefab.transform.localScale = rangedScale;

                Vector3 meleeScale = meleePrefab.transform.localScale;
                meleeScale.x = 1;
                meleePrefab.transform.localScale = meleeScale;
            }
        }
    }

    IEnumerator meleeAttack()
    {
        meleeOnCooldown = true;
        GameObject meleeProjectile = Instantiate(meleePrefab, transform.position, transform.rotation);
        Destroy(meleeProjectile, meleeDestroyDelay);
        yield return new WaitForSeconds(meleeCooldownTime);
        meleeOnCooldown = false;
    }

    IEnumerator rangedAttack()
    {
        rangedOnCooldown = true;
        gunshotAudio.Play();
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
        yield return new WaitForSeconds(rangedCooldownTime);
        rangedOnCooldown = false;
    }
}