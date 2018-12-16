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
    public GameObject shootPoints;
    private Transform[] shootPointsArray;
    public GameObject muzzleFlashPoint;
    public Light[] muzzleLights;
    public float maxAngle = 40;
    public float shootingRange = 30;
    public float maxDistance = 40;
    public int damageAmount = 1;
    private GameObject healthObj;
    private AudioSource[] machineGunNoise;
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
        healthObj = GameObject.FindWithTag("HealthObject");
        Debug.Log(target.name);
        machineGunNoise = shootPoints.GetComponentsInChildren<AudioSource>();
        hitSound = target.GetComponent<AudioSource>();
        explodeSound = GetComponent<AudioSource>();
        shootPointsArray = shootPoints.GetComponentsInChildren<Transform>();
        muzzleLights = muzzleFlashPoint.GetComponentsInChildren<Light>();
        foreach(Light l in muzzleLights)
        {
            l.enabled = false;
        }
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
        Debug.Log("Angle: " + angle + " Distance: " + distance + " Target Dir: " + targetDir);
        
        if (angle < maxAngle && distance <= maxDistance * 1.5f) {
            spotted = true;
        } else {
            spotted = false;
        }

        if (turret.transform.rotation.y <= 0 || gun.transform.rotation.z <= -2 || gun.transform.rotation.z >= 2)
            spotted = false;

        //Debug.DrawLine(transform.position, target.transform.position, Color.blue);
        
        
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
        
        if(spotted && !machineGunNoise[0].isPlaying && !machineGunNoise[1].isPlaying && !machineGunNoise[2].isPlaying && !machineGunNoise[3].isPlaying && !machineGunNoise[4].isPlaying && !machineGunNoise[5].isPlaying)// && distance <= shootingRange && distance > 0)
            shoot();

        
        Debug.Log(spotted);
        //Debug.DrawRay(shootPoint.transform.position, shootPoint.transform.TransformDirection(Vector3.forward)* 100, Color.cyan);
        //Debug.DrawRay(shootPointsArray[0].position, shootPointsArray[0].TransformDirection(Vector3.forward)* 100, Color.red);
        //Debug.DrawRay(shootPointsArray[1].position, shootPointsArray[1].TransformDirection(Vector3.forward)* 100, Color.green);
        //Debug.DrawRay(shootPointsArray[2].position, shootPointsArray[2].TransformDirection(Vector3.forward)* 100, Color.gray);
        //Debug.DrawRay(shootPointsArray[3].position, shootPointsArray[3].TransformDirection(Vector3.forward)* 100, Color.blue);
        //Debug.DrawRay(shootPointsArray[4].position, shootPointsArray[4].TransformDirection(Vector3.forward)* 100, Color.black);
        //Debug.DrawRay(shootPointsArray[5].position, shootPointsArray[5].TransformDirection(Vector3.forward)* 100, Color.cyan);
        

    }

    void shoot()
    {
        foreach (AudioSource sound in machineGunNoise)
        {
            if (!sound.isPlaying)
            {
                sound.Play();
            }
        }
        //GameObject flash = Instantiate(muzzleFlash, gun.transform);
        //RaycastHit shot;
        //if(Physics.Raycast(shootPoint.transform.position, shootPoint.transform.TransformDirection(Vector3.forward), out shot))
        //{
        //    float targetDistance = shot.distance;
        //    if (targetDistance < maxDistance)
        //    {
        //        if (!hitSound.isPlaying)
        //        {
        //            hitSound.Play();
        //        }
        //        healthObj.GetComponent<healthObject>().DoDamageOnPlayer(damageAmount);
        //    }
        //}

        foreach (Transform t in shootPointsArray)
        {
            //GameObject flash = Instantiate(muzzleFlash, t);
            RaycastHit shoot;
            if (Physics.Raycast(t.position, t.transform.TransformDirection(Vector3.forward), out shoot))
            {
                float targetDistance = shoot.distance;
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
        foreach (Light t in muzzleLights)
        {
            if (t.enabled)
            {
                t.enabled = false;
            }
            else
                t.enabled = true;
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
