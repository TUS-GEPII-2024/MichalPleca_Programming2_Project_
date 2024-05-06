using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class endCircle : MonoBehaviour
{
    private Animator winAnimator;
    public playerCollectibles playerCollectibles;
    public Canvas winUI;

    private bool playerInTrigger = false;

    public Light2D endCircleLight1;
    public Light2D endCircleLight2;
    public Light2D endCircleLight3;
    public Light2D endCircleLight4;
    public Light2D endCircleLight5;

    private void Start()
    {
        winAnimator = GetComponent<Animator> ();
    }

    private void Update()
    {
        if (playerInTrigger && playerCollectibles.eggCount >= 5 && Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("player Should Win");
            StartCoroutine(playerWon());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInTrigger = false;
        }
    }

    IEnumerator playerWon()
    {
        winAnimator.SetTrigger("endCircleLight");
        endCircleLight1.enabled = true;
        yield return new WaitForSeconds(0.35f);
        endCircleLight2.enabled = true;
        yield return new WaitForSeconds(0.35f);
        endCircleLight3.enabled = true;
        yield return new WaitForSeconds(0.35f);
        endCircleLight4.enabled = true;
        yield return new WaitForSeconds(0.35f);
        endCircleLight5.enabled = true;
        yield return new WaitForSeconds(1.25f);
        Time.timeScale = 0;
        winUI.enabled = true;
    }
}
