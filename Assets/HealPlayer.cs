﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPlayer : MonoBehaviour {
    private GameObject healObject;
    public GameObject healBox;
    public float minHealthGain = 10;
    public float maxHealthGain = 26;
    private bool playerHealed = false;
    public float radius = 5f;
    public AudioSource playPickUp;
    public AudioClip sound;
	// Use this for initialization
	void Start () {
        healObject = GameObject.FindGameObjectWithTag("HealthObject");
        playPickUp = GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update () {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider nearByObj in colliders)
        {
            if (nearByObj.tag.ToString().Equals("Player"))
            {
                if (!playerHealed)
                {
                    playerHealed = true;
                    playPickUp.PlayOneShot(sound);
                    healObject.GetComponent<healthObject>().DoHealOnPlayer((int)Random.Range(minHealthGain, maxHealthGain));
                    Destroy(healBox);
                    Destroy(gameObject, 5f);
                }
                break;
            }
        }
    }
}
