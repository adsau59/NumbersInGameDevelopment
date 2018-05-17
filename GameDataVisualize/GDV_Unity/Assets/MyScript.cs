using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyScript : MonoBehaviour {

    GDV_Core gDV;

    // Use this for initialization
    void Start () {

        /*gDV = new GDV_Core("127.0.0.1", 12345);

        gDV.graphConfig(new GDV_Core.DefaultGraphConfig("Distance", "Velocity", "r-", "b--"));*/


    }

    public float v = 5;

    public float v2 = 1;

    // Update is called once per frame
    void Update () {
        //gDV.addValues(transform.position.x, v, transform.position.y, v2);
        GDV_Core.DEFAULT.addAndSend(transform.position.x, v, transform.position.y, v2);

        v += 0.1f;
        v2 += 0.2f;

        transform.position = new Vector3(transform.position.x + v * Time.deltaTime, transform.position.y + v2 * Time.deltaTime, transform.position.z);


    }

    private void LateUpdate()
    {
        //gDV.send();
    }
}
