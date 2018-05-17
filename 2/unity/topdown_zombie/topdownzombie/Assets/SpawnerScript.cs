using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour {

    [SerializeField]
    private GameObject zombiePrefab;

    [SerializeField]
    private GameObject target;

    private Transform[] spawnPoint;


    [SerializeField]
    private float initialSpawnFrequency;
    [SerializeField]
    private float spawnIncreaseRate;
    [SerializeField]
    private float finalSpawnFrequency;
    private float killCountThreshold;

    [HideInInspector]
    public float killCount = 0;

    private float spawnFrequency;

    GDV_Core gdv;

	// Use this for initialization
	void Awake () {
        spawnPoint = transform.GetComponentsInChildren<Transform>();
        gdv = new GDV_Core(useDefaultConfig: false);
        gdv.graphConfig(new GDV_Core.DefaultGraphConfig("killCount", "spawnFrequency", "Zombie spawnFrequency vs killCount", "r-"));
	}

    void Start()
    {
        killCountThreshold = (finalSpawnFrequency - initialSpawnFrequency) / spawnIncreaseRate;

        spawnFrequency = initialSpawnFrequency;
        StartCoroutine(spawn());
    }

    // Update is called once per frame
    void Update () {

        spawnFrequency = killCount <= killCountThreshold ? spawnIncreaseRate * killCount + initialSpawnFrequency : finalSpawnFrequency;
        /*
        if (killCount <= killCountThreshold)
            spawnFrequency = spawnIncreaseRate * killCount + initialSpawnFrequency;
        else
            spawnFrequency = finalSpawnFrequency;
        */

        gdv.addAndSend(killCount, spawnFrequency);
    }

    IEnumerator spawn()
    {
        GameObject go = Instantiate(zombiePrefab, spawnPoint[Random.Range(0, spawnPoint.Length)].position, zombiePrefab.transform.rotation);
        go.GetComponent<ZombieAI>().target = target;
        yield return new WaitForSeconds(1 / spawnFrequency);
        StartCoroutine(spawn());
    }
}
