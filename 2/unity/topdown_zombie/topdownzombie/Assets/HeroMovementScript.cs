using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovementScript : MonoBehaviour {

    [SerializeField]
    private float maxSpeed;

    float xInput;
    float yInput;
    Vector3 currentSpeed;

    private Vector3 velocity;
    private Vector3 additionalVelocities;

    private Rigidbody rigidBody;

    public bool stun;

    private Vector3 fadingVelocity;
    private Vector3 fadingDir;
    private float rate;
    private float fadinVelMag;

    // Use this for initialization
    void Start () {
        currentSpeed = Vector3.zero;
        rigidBody = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update () {
        
        velocity = stun?Vector3.zero:new Vector3(Input.GetAxisRaw("Horizontal") * maxSpeed, 0, Input.GetAxisRaw("Vertical") * maxSpeed).normalized * maxSpeed;

        /*if(fadinVelMag > 0)
        {
            fadinVelMag -= rate * Time.deltaTime;
            fadingVelocity = fadingDir * fadinVelMag;
        }
        else
        {
            fadingVelocity = Vector3.zero;
        }*/

        rigidBody.velocity = velocity + additionalVelocities + fadingVelocity;

    }

    public void addVelocity(Vector3 velocity)
    {
        additionalVelocities += velocity;
    }

    /*public void addFadingVelocity(Vector3 dir, float initialVelocity, float fadeAwayTime)
    {
        fadinVelMag = initialVelocity;
        rate = initialVelocity / fadeAwayTime;
        fadingDir = dir;
    }*/
}
