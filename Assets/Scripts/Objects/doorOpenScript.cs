using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorOpenScript : MonoBehaviour
{
    public bool autoClose = true;
    public float autoCloseTimer = 2;

    public GameObject unloadRoom;
    public GameObject loadRoom;

    private Animator doorAnimator;
    private bool playerInDoor;
    void Start()
    {
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
        playerInDoor = true;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerInDoor = false;
    }

    IEnumerator doorOpenClose()
    {
        doorAnimator.SetTrigger("doorOpen");
        yield return new WaitForSeconds(autoCloseTimer);
        unloadRoom.SetActive(false);
        loadRoom.SetActive(true);
        doorAnimator.SetTrigger("doorOpen");
    }
}
