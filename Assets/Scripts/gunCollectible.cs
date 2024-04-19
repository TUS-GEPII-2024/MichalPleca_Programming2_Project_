using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class gunCollectible : MonoBehaviour
{
    private AudioSource gunPickupSound;
    private SpriteRenderer gunPickupSpriteRenderer;
    private Light2D gunPickupLight;
    private ShadowCaster2D gunPickupShadowCaster;
    private void Start()
    {
        gunPickupSpriteRenderer = GetComponent<SpriteRenderer>();
        gunPickupSound = GetComponent<AudioSource>();
        gunPickupLight = GetComponent<Light2D>();
        gunPickupShadowCaster = GetComponent<ShadowCaster2D>();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(gunPickup());
        }
    }

    IEnumerator gunPickup()
    {
        gunPickupSound.Play();
        playerAttack.instance.playerHasGun = true;
        gunPickupSpriteRenderer.enabled = false;
        gunPickupLight.enabled = false;
        gunPickupShadowCaster.enabled = false;
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
