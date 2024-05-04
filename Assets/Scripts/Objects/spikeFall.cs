using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class spikeFall : MonoBehaviour
{
    public float fallSpeed = 10f;
    public float destoryDelay = 1f;

    private GameObject spikeGameObject;
    private Transform spikeTransform;
    void Start()
    {
        spikeGameObject = gameObject.transform.parent.gameObject;
        spikeTransform = transform.parent;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(spikeFallDestroy());
        }
    }

    IEnumerator spikeFallDestroy()
    {
        spikeTransform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
        yield return new WaitForSeconds(destoryDelay);
        GameObject.Destroy(spikeGameObject);
    }
}
