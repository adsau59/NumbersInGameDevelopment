using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    [HideInInspector]
    public Vector3 shootDirection;

    [SerializeField]
    private float bulletSpeed = 30;

    [SerializeField]
    private float timeOutTime = 5.0f;

    [SerializeField]
    private int damage = 1;

	// Use this for initialization
	void Start () {
        StartCoroutine(timeout());
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += shootDirection * bulletSpeed * Time.deltaTime;
	}

    IEnumerator timeout()
    {
        yield return new WaitForSeconds(timeOutTime);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Zombie")
        {
            collision.gameObject.GetComponent<ZombieHealthScript>().hurt(damage);
        }

        Destroy(gameObject);
    }
}
