using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeMineBeep : MonoBehaviour {
    private AudioSource beeper;
    public GameObject mineLight;
    private Light light;
    public float lightBlinkDuration = 0.15f;
    private float timerOn;
    private float timerOff;
    private bool beepStarted = false;
    // Use this for initialization
    void Start () {
        beeper = GetComponent<AudioSource>();
        light = mineLight.GetComponent<Light>();
        light.enabled = false;
        timerOn = lightBlinkDuration;
        timerOff = lightBlinkDuration;

    }

    void Update()
    {
        if (beepStarted)
        {
            if (light.enabled == true)
            {
                timerOn -= Time.deltaTime;
                if (timerOn < 0)
                {
                    light.enabled = false;
                    timerOff = lightBlinkDuration;
                }
            }
            else if (light.enabled == false)
            {
                timerOff -= Time.deltaTime;
                if (timerOff < 0)
                {
                    light.enabled = true;
                    timerOn = lightBlinkDuration;
                }
            }
        }
    }

    public void Beep(AudioClip beepPlz)
    {
        if (!beeper.isPlaying)
        {
            beeper.PlayOneShot(beepPlz);
        }
        if(!beepStarted)
            light.enabled = true;
        beepStarted = true;

    }
}
