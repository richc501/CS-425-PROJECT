using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Translate : MonoBehaviour {
    //destination, using offsets
    public float speed;
    public Vector3[] offsets;
    private int index;
    private Vector3 nextPos;
    private bool invert;

    // Use this for initialization
    void Start () {
        index = 0;
        invert = false;
        nextPos = transform.position + offsets[0];
        index = 1;

    }

    // Update is called once per frame
    void Update () {
        //if we are at the destination, get the next position
        if (Vector3.Distance(transform.position,nextPos)<.0001){
            transform.position = nextPos;
            //if we are at start, no longer invert, and 
            if (index == 0){
                invert = false;
                //if we are at the end, start inverting positions
            }

            if (index == offsets.Length){
                invert = true;
            }
            //if we are going backwards, make all transformations negative
            if (invert){
                index--;
                nextPos = transform.position - offsets[index]; 
            }else{
                nextPos = transform.position + offsets[index];
                index++;
            }
        }

        //move towards the next position
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, nextPos, step);

    }
}
