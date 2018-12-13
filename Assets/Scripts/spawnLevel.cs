using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnLevel : MonoBehaviour {

    // Use this for initialization
    public GameObject level;
    public float yIncrease = 1f;
	void Start () {
        Vector3 pos = transform.position ;
        //Debug.Log(pos);
		GameObject lvl = Instantiate(level, pos, new Quaternion()) as GameObject;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
