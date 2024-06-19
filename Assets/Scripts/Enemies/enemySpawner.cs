using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public Transform spawnPoint3;

    public GameObject enemyPrefab1;
    public GameObject enemyPrefab2;
    public GameObject enemyPrefab3;

    public bool destroySpawner = true;
    public float destroyDelay = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (destroySpawner)
            {
                StartCoroutine(spawnerDestroy());
            }
            else
            {
                instantiateEnemies();
            }
        }
    }

    private void instantiateEnemies()
    {
        GameObject enemy1 = Instantiate(enemyPrefab1, spawnPoint1.position, transform.rotation);
        GameObject enemy2 = Instantiate(enemyPrefab2, spawnPoint2.position, transform.rotation);
        GameObject enemy3 = Instantiate(enemyPrefab3, spawnPoint3.position, transform.rotation);
    }

    IEnumerator spawnerDestroy()
    {
        instantiateEnemies();
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }
}
