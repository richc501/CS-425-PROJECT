using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMovement : MonoBehaviour {
    //destination, using offsets
    public float speed;
    private Quaternion left;
    private Quaternion right;
    private Quaternion nextRot;
    private Quaternion nextTilt;
    private bool spotted;
    private GameObject target;
    public GameObject turret;
    public GameObject gun;

    // Use this for initialization
    void Start(){
        spotted = false;
        nextTilt = Quaternion.AngleAxis(Random.Range(-30,30), Vector3.up);
        left = turret.transform.rotation * Quaternion.AngleAxis(90, Vector3.up);
        right = turret.transform.rotation * Quaternion.AngleAxis(-90, Vector3.up);
        nextRot = left;
        target = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update() {
        float step = speed * Time.deltaTime;
        Vector3 targetDir = target.transform.position - turret.transform.position;
        float angle = Vector3.Angle(targetDir, turret.transform.forward);
        if (angle < 40) {
            spotted = true;
        } else {
            spotted = false;
        }

        if (turret.transform.rotation.y <= 0 || gun.transform.rotation.z <= -2 || gun.transform.rotation.z >= 2)
            spotted = false;

        Debug.DrawRay(transform.position, target.transform.position, Color.blue);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 100, Color.red);

        RaycastHit hit;
        if (Physics.Linecast(transform.position, target.transform.position, out hit)) {
            
            if (hit.transform.tag != "Player")
            {
                spotted = false;
                Debug.Log(spotted);
                
            }
        }
        rotateGun(step);
        tiltGun(step);
        if(spotted)
            shoot();

        Debug.Log(spotted);
        Debug.DrawRay(transform.position, target.transform.position, Color.blue);
    }

    void shoot(){
        Debug.Log("SHOOT");
    }

    void rotateGun(float step){
        
        if (spotted)
        {
            var lookDir = target.transform.position - turret.transform.position;
            lookDir.y = 0; // keep only the horizontal direction
            turret.transform.rotation = Quaternion.LookRotation(lookDir);
        }
        else
        {
            if (turret.transform.rotation == left)
                nextRot = right;
            else if (turret.transform.rotation == right)
                nextRot = left;

            turret.transform.rotation = Quaternion.RotateTowards(turret.transform.rotation, nextRot, step);
        }
        
    }

    void tiltGun(float step){
        
        if (spotted){
            gun.transform.LookAt(target.transform.position);
        }
        else
        {
            if (gun.transform.rotation == nextTilt * turret.transform.rotation)
                nextTilt = Quaternion.AngleAxis(Random.Range(-30, 30), Vector3.right);
            gun.transform.rotation = Quaternion.RotateTowards(gun.transform.rotation, nextTilt * turret.transform.rotation, step);
        }
        
    }

    public void setSpotted(bool spotted){
        this.spotted = spotted;
    }
}
