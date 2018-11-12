using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject ball;
    public float speed;
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, ball.transform.position + new Vector3(4, 3, 0), step);
        transform.rotation = Quaternion.LookRotation((transform.position - ball.transform.position + new Vector3(0, -2, 0)).normalized * -1);
        //transform.SetPositionAndRotation(ball.transform.position + new Vector3(2, 2, 0), );
    }
}
