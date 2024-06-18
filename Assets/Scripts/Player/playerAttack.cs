using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class playerAttack : MonoBehaviour
{
    public static playerAttack instance;

    public GameObject meleePrefab;
    public GameObject rangedPrefab;

    public GameObject heldPistol;
    public Light2D muzzleFlash;

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
        if (playerHasGun && PlayerMovementController.instance.crouching == false)
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
        muzzleFlash.enabled = true;
        gunshotAudio.Play();
        heldPistol.SetActive(true);
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
        yield return new WaitForSeconds(0.05f);
        muzzleFlash.enabled = false;
        yield return new WaitForSeconds(rangedCooldownTime);
        heldPistol.SetActive(false);
        rangedOnCooldown = false;
    }
}