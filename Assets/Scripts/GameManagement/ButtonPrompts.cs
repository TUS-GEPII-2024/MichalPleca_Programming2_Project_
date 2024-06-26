using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPrompts : MonoBehaviour
{
    public KeyCode buttonPrompt;
    private SpriteRenderer buttonSpriteRenderer;
    public bool playerInRadius = false;
    public bool destroyOnLeave = true;
    public bool destroyOnButtonPress = true;
    void Start()
    {
        buttonSpriteRenderer = GetComponent<SpriteRenderer>();
        buttonSpriteRenderer.enabled = false;
    }

    private void Update()
    {
        if (playerInRadius==true && Input.GetKeyDown(buttonPrompt) && destroyOnButtonPress)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRadius = true;
            buttonSpriteRenderer.enabled = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && Input.GetKeyDown(buttonPrompt) && destroyOnButtonPress)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (destroyOnLeave)
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Player")
        {
            playerInRadius = false;
            buttonSpriteRenderer.enabled = false;
        }
    }
}
