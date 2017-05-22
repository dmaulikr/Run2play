using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class PrefabSpawner : MonoBehaviour {

	private float nextSpawn = 0;
	public Transform prefabtospawn;
	//public float spawnRate = 1;
	//public float randomDelay = 1;
    public AnimationCurve spawnCurve;
    public float curveLengthInSeconds = 60f;
    private float startTime;
    public float jitter = 0.5f;
	// Use this for initialization
	void Start () {
        startTime = Time.time;

    }
	
	// Update is called once per frame
	void Update () {
		if(Time.time > nextSpawn)
		{   
			Instantiate (prefabtospawn, transform.position, Quaternion.identity);
            //nextSpawn = Time.time + spawnRate + Random.Range (0, randomDelay);

            float curvePos = (Time.time - startTime) / curveLengthInSeconds;
            if (curvePos > 1f)
            {
                curvePos = 1f;
                startTime = Time.time;
            }
                nextSpawn = Time.time + spawnCurve.Evaluate(curvePos)+ Random.Range(-jitter,jitter);
            }
	}
}
