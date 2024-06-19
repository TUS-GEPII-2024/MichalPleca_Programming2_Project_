using System;
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
    public int rangedDamage = 1;

    public float meleeCooldownTime = 0.75f;
    public float rangedCooldownTime = 0.65f;

    public bool playerHasGun = false;
    public bool playerHasGunCollectible = false;

    private bool meleeOnCooldown;
    private bool rangedOnCooldown;
    [HideInInspector] public bool facingRight = true;
    private AudioSource gunshotAudio;

    [HideInInspector] public Transform parentTransform;
    private Vector2 parentScale;

    private Vector2 attackPointPos;
    private Vector2 attackPointScale;

    private Transform heldPistolTransform;
    private Vector2 heldPistolScale;

    private void Start()
    {
        gunshotAudio = GetComponent<AudioSource>();
        instance = this;

        parentTransform = transform.parent;
        parentScale = parentTransform.localScale;

        attackPointPos = transform.position;
        attackPointScale = transform.localScale;

        heldPistolTransform = heldPistol.transform;
        heldPistolScale = heldPistolTransform.localScale;
    }
    void Update()
    {
        mouseRotation();

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
        }
    }

    private void mouseRotation()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        if (Input.GetMouseButton(1) || (meleeOnCooldown || rangedOnCooldown))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 5.23f;
            Vector3 mouseDir = (mousePos - transform.position).normalized;
            float angle = Mathf.Atan2(mouseDir.y, mouseDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            if (mousePos.x < transform.position.x)
            {
                parentScale.x = -Mathf.Abs(parentScale.x);
                attackPointScale.x = -1;
                heldPistolScale.y = -0.75f;
            }
            else
            {
                parentScale.x = Mathf.Abs(parentScale.x);
                attackPointScale.x = 1;
                heldPistolScale.y = 0.75f;
            }
        }
        else if (horizontalInput < 0)
        {
            parentScale.x = -1;
        }
        else if (horizontalInput > 0)
        {
            parentScale.x = 1;
        }
        parentTransform.localScale = parentScale;
        heldPistolTransform.localScale = heldPistolScale;
        transform.localScale = attackPointScale;
    }

        IEnumerator meleeAttack()
        {
            meleeOnCooldown = true;
            mouseRotation();
            PlayerMovementController.instance.playerAnimator.SetBool("characterPunch", true);
            GameObject meleeProjectile = Instantiate(meleePrefab, transform.position, transform.rotation);
            Destroy(meleeProjectile, meleeDestroyDelay);
            yield return new WaitForSeconds(meleeCooldownTime);
            PlayerMovementController.instance.playerAnimator.SetBool("characterPunch", false);
            meleeOnCooldown = false;
        }

        IEnumerator rangedAttack()
        {
            rangedOnCooldown = true;
            mouseRotation();
            muzzleFlash.enabled = true;
            gunshotAudio.Play();
            heldPistol.SetActive(true);

            GameObject rangedProjectile = Instantiate(rangedPrefab, transform.position, transform.rotation);
            Rigidbody2D rangedProjectileRB = rangedProjectile.GetComponent<Rigidbody2D>();

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mouseDir = (mousePos - transform.position).normalized;

            rangedProjectileRB.AddForce(mouseDir * rangedProjectileForce, ForceMode2D.Impulse);

            Destroy(rangedProjectile, rangedDestroyDelay);
            yield return new WaitForSeconds(0.1f);
            muzzleFlash.enabled = false;
            yield return new WaitForSeconds(rangedCooldownTime);
            heldPistol.SetActive(false);
            rangedOnCooldown = false;
        }
    }
