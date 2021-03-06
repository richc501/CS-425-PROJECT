﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shootGun : MonoBehaviour {
    public int damageAmount = 1;
    public float targetDistance;
    public float maxDistance = 20;
    public LineRenderer lr;
    public RectTransform ammoClipText;
    public GameObject bulletHole;
    public GameObject bullet;
    // Use this for initialization
    void Start () {
        lr = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        int ammoClipSize = 0;
        int.TryParse(ammoClipText.GetComponent<Text>().text, out ammoClipSize);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * maxDistance, Color.green);
        if (ammoClipSize>0)
        {
            if (Input.GetButtonDown("Fire1"))
            {

                //GameObject Bullet = Instantiate(bullet, transform.position, new Quaternion());
                //Bullet.transform.rotation = transform.rotation;
                //Bullet.transform.Rotate(90, 0, 0);

                RaycastHit shot;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out shot))
                {
                    targetDistance = shot.distance;
                    //Vector3 endPoint = shot.point;
                    //lr.SetPosition(0, transform.position);
                    //lr.SetPosition(1, endPoint);
                    

                    if (targetDistance <= maxDistance)
                    {
                        GameObject hole = Instantiate(bulletHole, shot.point, Quaternion.FromToRotation(Vector3.up, shot.normal));
                        hole.transform.SetParent(shot.transform);
                        shot.transform.SendMessage("DoDamage", damageAmount, SendMessageOptions.DontRequireReceiver);
                    }

                }
            }
        }
	}

}
