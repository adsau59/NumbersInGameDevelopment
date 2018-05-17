using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : MonoBehaviour {


    [SerializeField]
    private float speed;

    private Vector3 direction;

    public GameObject target;

    [SerializeField]
    private int damage;

    [Header("Hero knockback")]
    [SerializeField]
    private float knockbackVelocity = 5;
    [SerializeField]
    private float knockbackTime = 1;

    Rigidbody rigidBody;

	// Use this for initialization
	void Awake() {
        rigidBody = GetComponent<Rigidbody>();
	}

    private Vector3 velocity;
    [HideInInspector]
    private bool stun = false;
    public Vector3 additionalVel;

    // Update is called once per frame
    void Update () {

        direction = (target.transform.position - transform.position).normalized;

        velocity = stun?Vector3.zero:direction * speed;
        

        rigidBody.velocity = velocity + additionalVel;
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<HealthScript>().hurt(damage, transform.position, knockbackVelocity, knockbackTime);
        }
    }
}
