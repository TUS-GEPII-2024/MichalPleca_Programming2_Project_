using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ladder : MonoBehaviour
{
    public GameObject playerObject;
    public float climbSpeed = 2f;
    public bool canClimb = false;

    private void Update()
    {
        if (canClimb)
        {
            if (Input.GetKey(KeyCode.W))
            {
                playerObject.transform.Translate(Vector2.up * climbSpeed * Time.deltaTime);
                playerObject.GetComponent<Rigidbody2D>().gravityScale = 0f;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                playerObject.transform.Translate(Vector2.down * climbSpeed * Time.deltaTime);
                playerObject.GetComponent<Rigidbody2D>().gravityScale = 0f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerObject = collision.gameObject;
            canClimb = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerObject.GetComponent<Rigidbody2D>().gravityScale = 2f;
            canClimb = false;
        }
    }
}