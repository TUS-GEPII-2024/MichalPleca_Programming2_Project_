using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikeFall : MonoBehaviour
{
    public float fallSpeed = 1f;
    void Start()
    {
        
    }

    
    void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
    }
}
