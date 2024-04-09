using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class playerCollectibles : MonoBehaviour
{
    public int eggCount = 0;
    public TextMeshProUGUI eggCountText;
    private bool eggCooldown = false;

    void Update()
    {
        eggCountText.text = eggCount.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Collectible" && !eggCooldown)
        {
            Destroy(collision.gameObject);
            StartCoroutine(eggCounter());
        }
    }

    IEnumerator eggCounter()
    {
        eggCooldown = true;
        eggCount++;
        yield return new WaitForSeconds(1);
        eggCooldown = false;
    }
}
