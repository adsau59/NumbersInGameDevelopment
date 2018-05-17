using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealthScript : MonoBehaviour {

    public int health;
    private SpawnerScript spawnerScript;

    private void Awake()
    {
        spawnerScript = GameObject.FindGameObjectWithTag("Spawner").GetComponent<SpawnerScript>();
    }

    public void hurt(int damage)
    {
        health -= damage;
        if (health <= 0)
            die();
    }

    public void die()
    {
        spawnerScript.killCount++;
        Destroy(gameObject);
    }
}
