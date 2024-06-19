using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class fallingFloor : MonoBehaviour
{
    private Rigidbody2D floorRB;
    private Animator floorAnimator;
    private ShadowCaster2D floorShadowCaster;
    private AudioSource floorAudioSource;
    private bool fallStarted = false;

    void Start()
    {
        floorRB = GetComponent<Rigidbody2D>();
        floorAnimator = GetComponent<Animator>();
        floorShadowCaster = GetComponent<ShadowCaster2D>();
        floorAudioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(floorFall());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !fallStarted)
        {
            StartCoroutine(floorFall());
        }
    }

    IEnumerator floorFall()
    {
        fallStarted = true;
        floorRB.constraints = RigidbodyConstraints2D.None;
        floorAudioSource.Play();
        floorAnimator.SetTrigger("floorFade");
        yield return new WaitForSeconds(0.5f);
        Destroy(floorAudioSource);
        floorShadowCaster.enabled = false;
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
