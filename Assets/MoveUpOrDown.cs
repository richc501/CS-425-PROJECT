using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpOrDown : MonoBehaviour {
    private int upOrDown;
    public float speed = 5;
    private Vector3 currentPos;
	// Use this for initialization
	void Start () {
        upOrDown = Random.Range(0, 2);
        currentPos = transform.position;
        updatePosition();

    }
	
	// Update is called once per frame
	void Update () {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, currentPos, step);
        updatePosition();
        if (transform.position.y<=-8)
        {
            upOrDown = 0;
        }
        else if(transform.position.y>=8)
        {
            upOrDown = 1;
        }


        
	}

    private void updatePosition()
    {
        switch (upOrDown)
        {
            case 0: //up
                currentPos.y += 16;
                break;
            case 1://down
                currentPos.y -= 16;
                break;
        }
    }
}
