using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralAnimationTrigger : MonoBehaviour
{
    public Animator objectAnimator;
    public string animatorTriggerString;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            objectAnimator.SetTrigger(animatorTriggerString);
        }
    }
}
