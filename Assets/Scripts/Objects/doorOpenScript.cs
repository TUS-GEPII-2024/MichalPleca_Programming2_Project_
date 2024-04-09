using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorOpenScript : MonoBehaviour
{
    public bool autoClose = true;
    public float autoCloseTimer = 2;
    public Animator doorAnimator;
    void Start()
    {
        doorAnimator = GetComponent<Animator>();
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
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

    IEnumerator doorOpenClose()
    {
        doorAnimator.SetTrigger("doorOpen");
        yield return new WaitForSeconds(autoCloseTimer);
        doorAnimator.SetTrigger("doorOpen");
    }
}
