using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playImpactSound : MonoBehaviour {

    public AudioSource playImpactSoundNow;
    public AudioClip[] defaultImpactSounds;
	// Use this for initialization
	void Start () {
        playImpactSoundNow = GetComponent<AudioSource>();
        playImpactSoundNow.PlayOneShot(defaultImpactSounds[Random.Range(0, defaultImpactSounds.Length)]);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
