using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletMove : MonoBehaviour {
    //public float bulletSpeed = 2f;
    //private float targetDistance;
    //public float maxDistance = 1;
    //public GameObject bulletHole;
    //public int damageAmount = 1;
    void Start () {
        //GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward) * 6;
        //Rigidbody Temporary_RigidBody;
        //Temporary_RigidBody = GetComponent<Rigidbody>();

        ////Tell the bullet to be "pushed" forward by an amount set by Bullet_Forward_Force.
        //Temporary_RigidBody.AddForce(transform.forward * bulletSpeed);

        //Basic Clean Up, set the Bullets to self destruct after 10 Seconds, I am being VERY generous here, normally 3 seconds is plenty.
        //Destroy(Temporary_Bullet_Handler, 10.0f);
    }
	
	// Update is called once per frame
	void Update () {

        //transform.Translate(-1 * transform.TransformDirection(Vector3.forward) * bulletSpeed);
        
        //RaycastHit shot;
        //if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out shot))
        //{
        //    targetDistance = shot.distance;
        //    if(targetDistance < maxDistance)
        //    {
        //        GameObject hole = Instantiate(bulletHole, shot.point, Quaternion.FromToRotation(Vector3.up, shot.normal));
        //        hole.transform.SetParent(shot.transform);
        //        shot.transform.SendMessage("DoDamage", damageAmount, SendMessageOptions.DontRequireReceiver);
        //        Destroy(gameObject, 10f);
        //    }
        //}
    }
}
