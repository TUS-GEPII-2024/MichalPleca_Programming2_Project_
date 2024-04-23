using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorOpenScript : MonoBehaviour
{
    public bool autoClose = true;
    public float autoCloseTimer = 1.5f;

    public GameObject unloadRoom;
    public GameObject loadRoom;

    private AudioSource doorOpenSound;
    private Animator doorAnimator;
    private bool playerInDoor;
    void Start()
    {
        doorAnimator = GetComponent<Animator>();
        doorOpenSound = GetComponent<AudioSource>();
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
        doorOpenSound.Play();
        PlayerMovementController.instance.isInputEnabled = false;
        yield return new WaitForSeconds(autoCloseTimer);
        doorAnimator.SetTrigger("doorOpen");
        PlayerMovementController.instance.isInputEnabled = true;
        unloadRoom.SetActive(false);
        loadRoom.SetActive(true);
    }
}
