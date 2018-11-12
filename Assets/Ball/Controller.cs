using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
    public float ballSpeed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //get direction
        float xSpeed = Input.GetAxis("Horizontal");
        float ySpeed = Input.GetAxis("Vertical");
        //get the physics stuffs
        Rigidbody body = GetComponent<Rigidbody>();
        //jump
        if (Input.GetKey(KeyCode.Space))
            body.AddForce(new Vector3(0, 30f, 0));

        body.AddTorque(new Vector3(xSpeed,0,ySpeed)*ballSpeed*Time.deltaTime);
        

    }
}
