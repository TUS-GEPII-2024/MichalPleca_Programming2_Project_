using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

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
    void Start()
    {
        raycastLayerMask = LayerMask.GetMask("FloorForRaycast");
        isInputEnabled = true;
        instance = this;
        jumpAmountStored = jumpAmount;
        playerRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isInputEnabled)
        {
            walk();
            jump();
        }
        else if (!isInputEnabled)
        {
            playerRB.velocity = Vector3.zero;
        }
    }

    private void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
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

            if (jumpAmountStored > 1)
            {
                playerRB.velocity = Vector3.up * jumpForce;
                jumpAmountStored--;
            }
            else if (canJump && jumpAmountStored <= 1)
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
                walkParticles.Play();
            }
            
            playableCharacterScale.x = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (canJump)
            {
                walkParticles.Play();
            }

            playableCharacterScale.x = -1;
        }
        else
        {
            walkParticles.Stop();
        }
        transform.localScale = playableCharacterScale;
    }
}

