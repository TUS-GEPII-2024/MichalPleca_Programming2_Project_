using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakableFloor : MonoBehaviour
{
    public ParticleSystem breakParticles;
    public GameObject floor;
    private BoxCollider2D floorCollider;

    private void Start()
    {
        floorCollider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(floorBreak());
        }
    }

    IEnumerator floorBreak()
    {
        Destroy(floor);
        breakParticles.Play();
        floorCollider.enabled = false;
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }
}
