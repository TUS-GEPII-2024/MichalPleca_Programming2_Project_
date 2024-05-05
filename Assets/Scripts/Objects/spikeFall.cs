using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class spikeFall : MonoBehaviour
{
    public float fallSpeed = 10f;
    public float destoryDelay = 1f;

    private bool playerDetected = false;

    private GameObject spikeGameObject;
    private Transform spikeTransform;
    void Start()
    {
        spikeGameObject = gameObject.transform.parent.gameObject;
        spikeTransform = transform.parent;
    }

    private void Update()
    {
        if (playerDetected)
        {
            spikeTransform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerDetected = true;
            StartCoroutine(spikeFallDestroy());
        }
    }

    IEnumerator spikeFallDestroy()
    {
        yield return new WaitForSeconds(destoryDelay);
        GameObject.Destroy(spikeGameObject);
    }
}
