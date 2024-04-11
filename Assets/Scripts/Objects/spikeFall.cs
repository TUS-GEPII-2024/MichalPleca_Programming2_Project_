using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class spikeFall : MonoBehaviour
{
    public float fallSpeed = 10f;
    public GameObject spikeTransform;
    void Start()
    {
        spikeTransform = transform.parent.gameObject;
    }

    void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
    }
}
