using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour {

    [SerializeField]
    int health;

    [SerializeField]
    private GameObject deathScreen;

    public void hurt(int hurt, Vector3 enemyPos, float knockbackVelocity, float knockbackTime)
    {
        health -= hurt;

        if(enemyPos != Vector3.zero)
            StartCoroutine(knockback(Vector3.Scale(new Vector3(1, 0, 1), (transform.position - enemyPos)).normalized, knockbackVelocity, knockbackTime));

        if(health <= 0)
        {
            die();
        }
    }

    private void die()
    {
        print("you died!!!");
        deathScreen.SetActive(true);
    }

    IEnumerator knockback(Vector3 dir, float knockbackVelocity, float knockbackTime)
    {
        HeroMovementScript movementScript = GetComponent<HeroMovementScript>();
        movementScript.stun = true;
        movementScript.addVelocity(dir * knockbackVelocity);
        yield return new WaitForSeconds(knockbackTime);
        movementScript.addVelocity(dir * knockbackVelocity * -1);
        movementScript.stun = false;
    }

}
