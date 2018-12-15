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
    public GameObject shootPoint;
    public float maxAngle = 40;
    public float shootingRange = 30;
    public float maxDistance = 40;
    public int damageAmount = 2;
    public GameObject healthObj;
    private AudioSource machineGunNoise;
    private AudioSource hitSound;
    private AudioSource explodeSound;
    // Use this for initialization
    void Start(){
        //timer = maxTimer;
        spotted = false;
        nextTilt = Quaternion.AngleAxis(Random.Range(-30,30), Vector3.up);
        left = turret.transform.rotation * Quaternion.AngleAxis(90, Vector3.up);
        right = turret.transform.rotation * Quaternion.AngleAxis(-90, Vector3.up);
        nextRot = left;
        target = GameObject.FindWithTag("Player");
        Debug.Log(target.name);
        machineGunNoise = shootPoint.GetComponent<AudioSource>();
        hitSound = target.GetComponent<AudioSource>();
        explodeSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        if(explodeSound.isPlaying)
        {
            return;
        }

        float step = speed * Time.deltaTime;
        Vector3 targetDir = target.transform.position - turret.transform.position;
        float angle = Vector3.Angle(targetDir, turret.transform.forward);
        float distance = targetDir.magnitude;
        if (angle < maxAngle) {
            spotted = true;
        } else {
            spotted = false;
        }

        if (turret.transform.rotation.y <= 0 || gun.transform.rotation.z <= -2 || gun.transform.rotation.z >= 2)
            spotted = false;

        Debug.DrawLine(transform.position, target.transform.position, Color.blue);
        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 100, Color.red);

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
        
        if(spotted && !machineGunNoise.isPlaying)// && distance <= shootingRange && distance > 0)
            shoot();

        
        Debug.Log(spotted);
        Debug.DrawRay(shootPoint.transform.position, shootPoint.transform.TransformDirection(Vector3.forward)* 100, Color.cyan);
    }

    void shoot(){

        if(!machineGunNoise.isPlaying)
        {
            machineGunNoise.Play();
        }
        RaycastHit shot;
        if(Physics.Raycast(shootPoint.transform.position, shootPoint.transform.TransformDirection(Vector3.forward), out shot))
        {
            float targetDistance = shot.distance;
            if (targetDistance < maxDistance)
            {
                if (!hitSound.isPlaying)
                {
                    hitSound.Play();
                }
                healthObj.GetComponent<healthObject>().DoDamageOnPlayer(damageAmount);
            }
        }
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
