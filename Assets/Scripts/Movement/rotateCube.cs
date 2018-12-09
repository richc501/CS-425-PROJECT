using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateCube : MonoBehaviour {

    // Use this for initialization
    int r;
	void Start () {
		r = Random.Range(0, 2);
	}
	
	// Update is called once per frame
	void Update () {
        
        switch(r)
        {
            case 0:
                transform.Rotate(Vector3.right * 0.5f);
                break;
            case 1:
                transform.Rotate(Vector3.left * 0.5f);
                break;
        }
        
	}
}
