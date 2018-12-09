using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFire : MonoBehaviour {

    AudioSource gunSound;
    Animation anmi;
    public GameObject muzzleFlash;
    public GameObject shell;
    //public Transform flashSpawnPoint;
	// Use this for initialization
	void Start () {
        gunSound = GetComponent<AudioSource>();
        anmi = GetComponent<Animation>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire1"))
        {
            anmi.Play("Fire");
            gunSound.Play();
            GameObject flash = Instantiate(muzzleFlash, this.transform);
            GameObject s = Instantiate(shell, this.transform);
            
            Debug.Log("Before seting pos: " + flash.transform.position);
            //flash.transform.position = new Vector3(0, 0, 0);
            Debug.Log("after seting pos: " + flash.transform.position);

            //flash.transform.SetParent(flashSpawnPoint);

            
        }
	}
}
