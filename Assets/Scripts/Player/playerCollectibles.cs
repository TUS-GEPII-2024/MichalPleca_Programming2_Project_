using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class playerCollectibles : MonoBehaviour
{
    public int eggCount = 0;
    public int boneCount = 0;
    public TextMeshProUGUI eggCountText;
    public TextMeshProUGUI boneCountText;
    private bool eggCooldown = false;

    void Update()
    {
        eggCountText.text = eggCount.ToString();
        boneCountText.text = boneCount.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Collectible" && !eggCooldown)
        {
            Destroy(collision.gameObject);
            StartCoroutine(eggCounter());
        }
        if (collision.gameObject.tag == "BoneCollectible" && !eggCooldown)
        {
            Destroy(collision.gameObject);
            StartCoroutine(boneCounter());
        }
    }

    IEnumerator eggCounter()
    {
        eggCooldown = true;
        eggCount++;
        yield return new WaitForSeconds(1);
        eggCooldown = false;
    }

    IEnumerator boneCounter()
    {
        eggCooldown = true;
        boneCount++;
        yield return new WaitForSeconds(1);
        eggCooldown = false;
    }
}
