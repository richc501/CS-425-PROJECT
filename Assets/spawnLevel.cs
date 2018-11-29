using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnLevel : MonoBehaviour {

    // Use this for initialization
    public GameObject level;
	void Start () {
        Vector3 pos = transform.position + new Vector3(0, 101, 0);
        Debug.Log(pos);
		GameObject lvl = Instantiate(level, pos, new Quaternion()) as GameObject;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
