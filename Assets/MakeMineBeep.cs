using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeMineBeep : MonoBehaviour {
    private AudioSource beeper;
	// Use this for initialization
	void Start () {
        beeper = GetComponent<AudioSource>();
	}

    public void Beep(AudioClip beepPlz)
    {
        if (!beeper.isPlaying)
        {
            beeper.PlayOneShot(beepPlz);
        }
    }
}
