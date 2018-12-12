using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletMove : MonoBehaviour {
    public float bulletSpeed = 100000f;
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        
        transform.Translate(-1 * transform.TransformDirection(Vector3.forward) * Time.deltaTime * bulletSpeed);

    }
}
