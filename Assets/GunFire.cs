using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunFire : MonoBehaviour {
    private int ammoInClip = 20;
    public int ammoInBag = 999;
    AudioSource gunSound;
    public AudioClip gunDrySound;
    public AudioClip gunReloadSound;
    public AudioClip gunReloadSoundFail;
    Animation anmi;
    public GameObject muzzleFlash;
    public GameObject shell;
    public RectTransform ammoClipText;
    public RectTransform ammoBagText;
    //public Transform flashSpawnPoint;
	// Use this for initialization
	void Start () {
        gunSound = GetComponent<AudioSource>();
        anmi = GetComponent<Animation>();
        ammoBagText.GetComponent<Text>().text = ammoInBag.ToString();
        ammoClipText.GetComponent<Text>().text = ammoInClip.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire1"))
        {
            if (ammoInClip > 0)
            { 
                ammoInClip--;
                ammoClipText.GetComponent<Text>().text = ammoInClip.ToString();
                anmi.Play("Fire");
                gunSound.Play();
                GameObject flash = Instantiate(muzzleFlash, this.transform);
                GameObject s = Instantiate(shell, this.transform);
            }
            else
            {
                anmi.Play("FireEmpty");
                gunSound.PlayOneShot(gunDrySound);
                gameObject.SendMessage("ChangeBool", ammoInClip, SendMessageOptions.DontRequireReceiver);
            }
            
        }
        if(ammoInClip < 20)
        {
            if(Input.GetButtonDown("Reload"))
            {
                if (ammoInBag > 0)
                {
                    gunSound.PlayOneShot(gunReloadSound);
                    anmi.Play("StandardReload");
                    int ammoBeforeReload = ammoInClip;
                    if (ammoInBag < 20)
                    {
                        ammoInClip = ammoInBag;
                        ammoInBag = 0;
                    }
                    else
                    {
                        ammoInClip = 20;
                        ammoInBag = ammoInBag - (ammoInClip - ammoBeforeReload);
                    }
                    ammoBagText.GetComponent<Text>().text = ammoInBag.ToString();
                    ammoClipText.GetComponent<Text>().text = ammoInClip.ToString();
                }
                else
                {
                    gunSound.PlayOneShot(gunReloadSoundFail);
                    anmi.Play("ReloadEmpty");
                }
            }
        }
	}

    public void IncreaseAmmo(int amountIncreased)
    {
        ammoInBag += amountIncreased;
        if(ammoInBag>999)
        {
            ammoInBag = 999;
        }
        ammoBagText.GetComponent<Text>().text = ammoInBag.ToString();
    }
}

