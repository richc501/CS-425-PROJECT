using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blowUpMine : MonoBehaviour {
    public int mineDamage = 10;
    private bool hasExploded = false;
    private bool playerNearBy = false;
    private bool playerDamaged = false;
    public float maxtime = 10f;
    private float timer = 5f;
    public float radius = 10f;
    public GameObject Explosion;
    public GameObject healObject;
    public GameObject mineObj;
    private AudioSource mineSoundEmmiter;
    public AudioClip[] mineSounds;
	// Use this for initialization
	void Start () {
        mineSoundEmmiter = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        if(playerNearBy)
        {
            timer -= Time.deltaTime;
        }
        if (!hasExploded)
        {
            foreach (Collider nearByObj in colliders)
            {
                if (nearByObj.tag.ToString().Equals("Player"))
                {
                    playerNearBy = true;
                    mineObj.GetComponent<MakeMineBeep>().Beep(mineSounds[0]);
                    break;
                }
                else
                {
                    playerNearBy = false;
                    timer = maxtime;
                }
            }
        }

        if(timer<0&&!hasExploded)
        {
            //mineSoundEmmiter.Stop();
            
            Explode();
            Destroy(mineObj);
            Destroy(gameObject, 60f);
        }
	}
    
    void Explode()
    {
        hasExploded = true;
        GameObject boom = Instantiate(Explosion, transform.position, transform.rotation);
        mineSoundEmmiter.PlayOneShot(mineSounds[1]);
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider nearByObj in colliders)
        {
            if (nearByObj.tag.ToString().Equals("Player"))
            {
                if (!playerDamaged)
                {
                    playerDamaged = true;
                    healObject.GetComponent<healthObject>().DoDamageOnPlayer(mineDamage);
                }
                break;
            }
        }
        Destroy(boom, 2f);

    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //        hasExploded = true;
    //        Instantiate(Explosion, transform.position, transform.rotation);
    //        transform.SendMessage("DoDamage", mineDamage, SendMessageOptions.DontRequireReceiver);
    //        Destroy(gameObject);
    //}
}
