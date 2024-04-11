using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    public GameObject meleePrefab;
    public GameObject rangedPrefab;

    public float meleeDestroyDelay = 1;
    public float rangedDestroyDelay = 2;

    public float rangedProjectileForce = 1;

    public float meleeCooldownTime = 0.75f;
    public float rangedCooldownTime = 0.75f;

    private bool meleeOnCooldown;
    private bool rangedOnCooldown;
    private bool facingRight = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !meleeOnCooldown || Input.GetKeyDown(KeyCode.RightShift) && !meleeOnCooldown)
        {
            StartCoroutine(meleeAttack());
        }
        if (Input.GetKey(KeyCode.Mouse0) && !rangedOnCooldown || Input.GetKey(KeyCode.RightControl) && !rangedOnCooldown)
        {
            StartCoroutine(rangedAttack());
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            facingRight = false;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            facingRight = true;
        }
    }

    IEnumerator meleeAttack()
    {
        meleeOnCooldown = true;
        GameObject meleeProjectile = Instantiate(meleePrefab, transform.position, transform.rotation);
        Vector3 meleeScale = meleeProjectile.transform.localScale;
        if (facingRight)
        {
            meleeScale.x = -1;
        }
        else
        {
            meleeScale.x = 1;
        }
        meleeProjectile.transform.localScale = meleeScale;
        Destroy(meleeProjectile, meleeDestroyDelay);
        yield return new WaitForSeconds(meleeCooldownTime);
        meleeOnCooldown = false;
    }

    IEnumerator rangedAttack()
    {
        rangedOnCooldown = true;
        GameObject rangedProjectile = Instantiate(rangedPrefab, transform.position, transform.rotation);
        Rigidbody2D rangedProjectileRB = rangedProjectile.GetComponent<Rigidbody2D>();
        if (facingRight)
        {
            rangedProjectileRB.AddForce(Vector2.right * rangedProjectileForce, ForceMode2D.Impulse);
        }
        else
        {
            rangedProjectileRB.AddForce(Vector2.left * rangedProjectileForce, ForceMode2D.Impulse);
        }
        Destroy(rangedProjectile, rangedDestroyDelay);
        yield return new WaitForSeconds(rangedCooldownTime);
        rangedOnCooldown = false;
    }
}