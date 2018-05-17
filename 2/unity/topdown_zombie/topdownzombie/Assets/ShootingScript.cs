using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour {

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private LayerMask ground;

    [SerializeField]
    private float yoffset;

    [SerializeField]
    private float xzOffset;

	// Use this for initialization
	void Start () {
		
	}
	    
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, ground))
            {
                Vector3 spawn = transform.position + (hit.point - transform.position).normalized * xzOffset + Vector3.up * yoffset;

                GameObject bullet = Instantiate(bulletPrefab, spawn, bulletPrefab.transform.rotation);
                bullet.GetComponent<BulletScript>().shootDirection = Vector3.Scale(new Vector3(1,0,1), (bullet.transform.position - transform.position)).normalized;
            }
        }

    }
}
