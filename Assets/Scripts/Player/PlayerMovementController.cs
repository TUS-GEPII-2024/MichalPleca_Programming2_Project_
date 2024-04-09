using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public float speed = 1;
    public float jumpForce = 1;
    private Rigidbody2D playerRB;
    void Start()
    {
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
        if(Input.GetKeyDown(KeyCode.Space))
        {
            playerRB.velocity = Vector3.up * jumpForce;
        }
        transform.localScale = playableCharacterScale;
    }
}
