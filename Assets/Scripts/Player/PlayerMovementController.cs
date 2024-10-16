using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerMovementController : MonoBehaviour
{
    //Player flipping is located in playerAttack script in mouseRotation function in order to work with aiming with the mouse.

    public static PlayerMovementController instance;
    [HideInInspector] public Rigidbody2D playerRB;
    private LayerMask raycastLayerMask;
    private LayerMask ceilingLayerMask;

    [HideInInspector] public bool isInputEnabled;
    private bool canJump = true;

    public float speed = 1;
    public float jumpForce = 1;

    public float jumpAmount = 1;
    [HideInInspector] public float jumpAmountStored;

    public bool dashEnabled = false;
    public Vector2 dashDistance;

    public float dashCooldown = 4f;
    public float dashInputCooldown = 0.25f;

    private bool dashOnCooldown = false;
    private bool dashTimerOnCooldown = false;

    public ParticleSystem walkParticles;
    public ParticleSystem dashParticles;
    [HideInInspector] public Animator playerAnimator;

    private AudioSource walkSound;
    private bool walkSoundPlay;

    private ShadowCaster2D shadowCaster;
    private BoxCollider2D standingCollider;
    private CapsuleCollider2D crouchCollider;

    public TextMeshProUGUI dashCooldownText;
    private int dashCooldownTimer = 0;

    [HideInInspector] public bool crouching;
    void Start()
    {
        walkSoundPlay = false;
        crouching = false;
        raycastLayerMask = LayerMask.GetMask("FloorForRaycast");
        ceilingLayerMask = LayerMask.GetMask("CeilingForRaycast");
        isInputEnabled = true;
        instance = this;
        jumpAmountStored = jumpAmount;
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        standingCollider = GetComponent<BoxCollider2D>();
        crouchCollider = GetComponent<CapsuleCollider2D>();
        shadowCaster = GetComponent<ShadowCaster2D>();
        walkSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isInputEnabled)
        {
            walk();
            jump();
            crouch();
            startDash();
            dashCooldownText.text = dashCooldownTimer.ToString();
        }
    }

    void startDash()
    {
        if (Input.GetKeyDown(KeyCode.Q) && dashOnCooldown == false && dashEnabled)
        {
            StartCoroutine(dash());
        }
        else if (dashOnCooldown == true && dashTimerOnCooldown == false)
        {
            StartCoroutine(dashTimer());
        }
    }

    IEnumerator dash()
    {
        dashOnCooldown = true;
        isInputEnabled = false;

        Vector2 dashForce;
        dashForce.x = playerRB.velocity.x * dashDistance.x;
        dashForce.y = dashDistance.y;
        playerRB.AddForce(dashForce, ForceMode2D.Impulse);
        dashParticles.Play();

        yield return new WaitForSeconds(dashInputCooldown);
        isInputEnabled = true;

        yield return new WaitForSeconds(dashCooldown);
        dashOnCooldown = false;
    }

    IEnumerator dashTimer()
    {
        dashTimerOnCooldown = true;
        dashCooldownTimer = 5;
        yield return new WaitForSeconds(1);
        dashCooldownTimer = 4;
        yield return new WaitForSeconds(1);
        dashCooldownTimer = 3;
        yield return new WaitForSeconds(1);
        dashCooldownTimer = 2;
        yield return new WaitForSeconds(1);
        dashCooldownTimer = 1;
        yield return new WaitForSeconds(1);
        dashCooldownTimer = 0;
        dashTimerOnCooldown = false;
    }

    private void crouch()
    {
        RaycastHit2D ceilingDetect = Physics2D.Raycast(transform.position, -Vector2.down, 1, ceilingLayerMask);
        //Debug.DrawRay(transform.position, -Vector2.down * 1, Color.blue, 5);
        if (Input.GetKeyDown(KeyCode.S) && crouching == false)
        {
            canJump = false;
            standingCollider.enabled = false;
            crouchCollider.enabled = true;
            shadowCaster.enabled = false;
            crouching = true;
            playerAnimator.SetBool("characterCrouch", true);
        }
        else if (Input.GetKeyDown(KeyCode.S) && crouching == true && ceilingDetect.collider == null)
        {
            canJump = true;
            standingCollider.enabled = true;
            crouchCollider.enabled = false;
            shadowCaster.enabled = true;
            crouching = false;
            playerAnimator.SetBool("characterCrouchIdle", false);
            playerAnimator.SetBool("characterCrouch", false);
        }

        if (playerRB.velocity.y == 0 && crouching)
        {
            playerAnimator.SetBool("characterCrouchIdle", true);
        }
        else
        {
            playerAnimator.SetBool("characterCrouchIdle", false);
        }
    }

    private void jump()
    {

        RaycastHit2D groundDetect = Physics2D.Raycast(transform.position, -Vector2.up, 1.5f, raycastLayerMask);
        //Debug.DrawRay(transform.position, -Vector2.up * 1.5f, Color.red, 5);
        if (groundDetect.collider == null)
        {
            //Debug.Log("Raycast DID NOT hit something");
            canJump = false;
        }
        else
        {
            //Debug.Log("Raycast hit something");
            canJump = true;
            jumpAmountStored = jumpAmount;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpAmountStored > 1 && !crouching)
            {
                playerRB.velocity = Vector3.up * jumpForce;
                jumpAmountStored--;
                walkParticles.Stop();
            }
            else if (canJump && jumpAmountStored <= 1 && !crouching)
            {
                playerRB.velocity = Vector3.up * jumpForce;
                jumpAmountStored = jumpAmount;
                walkParticles.Stop();
            }
        }
    }

    private void walk()
    {   
        //gets horizontal input, then multiplies that by the speed variable
        float horizontalMovement = Input.GetAxis("Horizontal") * speed;

        //speed variable is already attached to the horizontal input
        //below sets newVelocity.x to the horizontal input, which is basically the speed and direction
        //the player velocity is then set to the newVelocity velocity
        Vector2 newVelocity;
        newVelocity.x = horizontalMovement;
        newVelocity.y = playerRB.velocity.y;
        playerRB.velocity = newVelocity;



        if (Input.GetKey(KeyCode.D))
        {
            if (canJump)
            {
                playerAnimator.SetBool("characterIdle", false);
                walkParticles.Play();
                if (!walkSoundPlay)
                {
                    StartCoroutine(playWalkSound());
                }
            }
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (canJump)
            {
                playerAnimator.SetBool("characterIdle", false);
                walkParticles.Play();
                if (!walkSoundPlay)
                {
                    StartCoroutine(playWalkSound());
                }
            }
        }
        else
        {
            playerAnimator.SetBool("characterIdle", true);
            walkParticles.Stop();
            walkSound.Stop();
        }
    }

    IEnumerator playWalkSound()
    {
        walkSoundPlay = true;
        walkSound.Play();
        yield return new WaitForSeconds(0.45f);
        walkSoundPlay = false;
    }
}

