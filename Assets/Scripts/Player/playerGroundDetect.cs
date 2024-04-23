using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerGroundDetect : MonoBehaviour
{
    public static playerGroundDetect instance;

    [HideInInspector] public bool canJump = true;

    private void Start()
    {
        instance = this;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            canJump = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            canJump = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            canJump = false;
        }
    }
}
