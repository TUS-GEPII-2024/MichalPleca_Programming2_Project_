using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject playerObject;
    private Transform playerTransform;
    public float cameraOffset = 2;
    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerTransform = playerObject.transform;
    }

    void Update()
    {
        Vector3 cameraPosition = new Vector3(playerTransform.position.x, playerTransform.position.y + cameraOffset, transform.position.z);
        transform.position = cameraPosition;
    }
}
