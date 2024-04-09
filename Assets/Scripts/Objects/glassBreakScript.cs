using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class glassBreakScript : MonoBehaviour
{
    public Animator glassAnimator;
    void Start()
    {
        glassAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            glassAnimator.SetTrigger("glassBreak");
        }
    }
}
