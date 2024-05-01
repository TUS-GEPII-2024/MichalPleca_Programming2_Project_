using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class glassBreakScript : MonoBehaviour
{
    public Animator glassAnimator;
    private AudioSource glassAudioSource;
    private BoxCollider2D glassCollider;
    void Start()
    {
        glassAnimator = GetComponent<Animator>();
        glassAudioSource = GetComponent<AudioSource>();
        glassCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet") || collision.CompareTag("playerFist"))
        {
            StartCoroutine(glassBreaking());
        }
    }

    IEnumerator glassBreaking()
    {
        glassAnimator.SetTrigger("glassBreak");
        glassAudioSource.Play();
        glassCollider.enabled = false;
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }
}
