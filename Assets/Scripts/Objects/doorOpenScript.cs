using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorOpenScript : MonoBehaviour
{
    public bool autoClose = true;
    public float autoCloseTimer = 1.5f;

    public GameObject unloadRoom;
    public GameObject loadRoom;

    public PlayerMovementController playerMovement;
    private float playerMovementSpeed;

    private Animator doorAnimator;
    private bool playerInDoor;
    void Start()
    {
        playerMovement = GameObject.FindObjectOfType<PlayerMovementController>();
        playerMovementSpeed = playerMovement.speed;
        doorAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && playerInDoor)
        {
            if(autoClose)
            {
                StartCoroutine(doorOpenClose());
            }
            else if (!autoClose)
            {
                doorAnimator.SetTrigger("doorOpen");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        { 
            playerInDoor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInDoor = false;
        }
    }

    IEnumerator doorOpenClose()
    {
        doorAnimator.SetTrigger("doorOpen");
        playerMovement.speed = 0;
        yield return new WaitForSeconds(autoCloseTimer);
        doorAnimator.SetTrigger("doorOpen");
        playerMovement.speed = playerMovementSpeed;
        unloadRoom.SetActive(false);
        loadRoom.SetActive(true);
    }
}
