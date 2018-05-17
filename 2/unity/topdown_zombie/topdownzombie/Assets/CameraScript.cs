using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    Transform hero;

    [SerializeField]
    private Vector3 offset;

	// Use this for initialization
	void Awake () {
        hero = GameObject.FindGameObjectWithTag("Player").transform;

        if (offset == Vector3.zero)
            offset = transform.position - hero.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = hero.position + offset;
	}
}
