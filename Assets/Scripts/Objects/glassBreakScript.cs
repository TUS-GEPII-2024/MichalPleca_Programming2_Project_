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
        if (collision.CompareTag("Bullet") || collision.CompareTag("playerFist"))
        {
            StartCoroutine(glassBreaking());
        }
    }

    IEnumerator glassBreaking()
    {
        glassAnimator.SetTrigger("glassBreak");
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
