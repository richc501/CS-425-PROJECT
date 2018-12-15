using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GivePlayerAmmo : MonoBehaviour {
    public float radius = 5f;
    private bool playerAmmoReceived = false;
    private AudioSource playPickUp;
    public AudioClip sound;
    private GameObject gunObject;
    public GameObject ammo;
    public float minAmmoGain = 20;
    public float maxAmmoGain = 60;
    // Use this for initialization
    void Start () {
        gunObject = GameObject.FindWithTag("Pistol");
        playPickUp = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider nearByObj in colliders)
        {
            if (nearByObj.tag.ToString().Equals("Player"))
            {
                if (!playerAmmoReceived)
                {
                    playerAmmoReceived = true;
                    playPickUp.PlayOneShot(sound);
                    gunObject.GetComponent<GunFire>().IncreaseAmmo((int)Random.Range(minAmmoGain, maxAmmoGain));
                    Destroy(ammo);
                    Destroy(gameObject, 5f);
                }
                break;
            }
        }
    }
}
