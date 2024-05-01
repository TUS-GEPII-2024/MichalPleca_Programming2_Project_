using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerMovementController : MonoBehaviour
{
    public static PlayerMovementController instance;
    [HideInInspector] public Rigidbody2D playerRB;
    private LayerMask raycastLayerMask;

    [HideInInspector] public bool isInputEnabled;
    private bool canJump = true;

    public float speed = 1;
    public float jumpForce = 1;

    public float jumpAmount = 1;
    [HideInInspector] public float jumpAmountStored;

    public ParticleSystem walkParticles;
    [HideInInspector] public Animator playerAnimator;

    private AudioSource walkSound;
    private bool walkSoundPlay;

    private ShadowCaster2D shadowCaster;
    private BoxCollider2D standingCollider;
    private CapsuleCollider2D crouchCollider;
    private bool crouching;
    void Start()
    {
        walkSoundPlay = false;
        crouching = false;
        raycastLayerMask = LayerMask.GetMask("FloorForRaycast");
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
        }
        else if (!isInputEnabled)
        {
            playerRB.velocity = Vector3.zero;
        }
    }

    private void crouch()
    {
        if (Input.GetKeyDown(KeyCode.S) && crouching == false)
        {
            canJump = false;
            standingCollider.enabled = false;
            crouchCollider.enabled = true;
            shadowCaster.enabled = false;
            crouching = true;
            playerAnimator.SetBool("characterCrouch", true);
        }
        else if (Input.GetKeyDown(KeyCode.S) && crouching == true)
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
        Debug.DrawRay(transform.position, -Vector2.up * 1.5f, Color.red, 5);
        if (groundDetect.collider == null)
        {
            Debug.Log("Raycast DID NOT hit something");
            canJump = false;
        }
        else
        {
            Debug.Log("Raycast hit something");
            canJump = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpAmountStored > 1 && !crouching)
            {
                playerRB.velocity = Vector3.up * jumpForce;
                jumpAmountStored--;
            }
            else if (canJump && jumpAmountStored <= 1 && !crouching)
            {
                playerRB.velocity = Vector3.up * jumpForce;
                jumpAmountStored = jumpAmount;
            }
        }
    }

    private void walk()
    {
        Vector2 playableCharacterScale = transform.localScale;

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

            playableCharacterScale.x = 1;
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

            playableCharacterScale.x = -1;
        }
        else
        {
            playerAnimator.SetBool("characterIdle", true);
            walkParticles.Stop();
            walkSound.Stop();
        }
        transform.localScale = playableCharacterScale;
    }

    IEnumerator playWalkSound()
    {
        walkSoundPlay = true;
        walkSound.Play();
        yield return new WaitForSeconds(0.45f);
        walkSoundPlay = false;
    }
}

