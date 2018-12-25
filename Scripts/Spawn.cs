using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {
       
    public GameObject Dragon;
    public float spawnTime = 3f; 
    public float spawnDelay = 2f;
    public float direction;

    void Start () {
        InvokeRepeating("Spawn1", spawnDelay, spawnTime);
    }
	
	// Update is called once per frame
	void Spawn1() {
        Vector3 RandPos = new Vector3(0, Random.Range(0, 4), 0);
        GameObject drag = Instantiate(Dragon, transform.position + RandPos, Quaternion.identity);
        drag.GetComponent<Enemy>().direction = direction;
    }
}
