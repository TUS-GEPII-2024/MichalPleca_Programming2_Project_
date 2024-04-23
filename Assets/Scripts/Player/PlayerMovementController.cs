using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public static PlayerMovementController instance;
    public float speed = 1;

    public float jumpForce = 1;
    public float jumpAmount = 1;
    [HideInInspector]
    public float jumpAmountStored;
    private bool canJump = true;

    private Rigidbody2D playerRB;
    void Start()
    {
        instance = this;
        jumpAmountStored = jumpAmount;
        playerRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 playableCharacterScale = transform.localScale;
        if (Input.GetKey(KeyCode.D))
        {
            playableCharacterScale.x = 1;
            transform.position += Vector3.right * speed * Time.deltaTime;
        }   
        else if (Input.GetKey(KeyCode.A))
        {
            playableCharacterScale.x = -1;
            transform.position += Vector3.left * speed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
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
        transform.localScale = playableCharacterScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            canJump = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            canJump = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            canJump = false;
        }
    }
}
