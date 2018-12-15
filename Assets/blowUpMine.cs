using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blowUpMine : MonoBehaviour {
    public int mineDamage = 10;
    private bool hasExploded = false;
    private bool playerNearBy = false;
    private bool playerDamaged = false;
    public float maxtime = 3f;
    private float timer;
    public float checkRadius = 2f;
    public float blastRadius = 10f;
    public GameObject Explosion;
    private GameObject healObject;
    public GameObject mineObj;
    private AudioSource mineSoundEmmiter;
    public AudioClip[] mineSounds;

	// Use this for initialization
	void Start () {
        healObject = GameObject.FindWithTag("HealthObject");
        mineSoundEmmiter = GetComponent<AudioSource>();
        timer = maxtime;
	}
	
	// Update is called once per frame
	void Update () {
        Collider[] colliders = Physics.OverlapSphere(transform.position, checkRadius);
        if(playerNearBy)
        {
            timer -= Time.deltaTime;
            if(!hasExploded)
                mineObj.GetComponent<MakeMineBeep>().Beep(mineSounds[0]);
        }
        if (!hasExploded)
        {
            foreach (Collider nearByObj in colliders)
            {
                if (nearByObj.tag.ToString().Equals("Player"))
                {
                    playerNearBy = true;
                    
                    break;
                }
                //else
                //{
                //    playerNearBy = false;
                //    timer = maxtime;
                //}
            }
        }

        if(timer<0&&!hasExploded)
        {
            //mineSoundEmmiter.Stop();
            
            Explode();
            Destroy(mineObj);
            Destroy(gameObject, 10f);
        }
	}
    
    public void Explode()
    {
        hasExploded = true;
        GameObject boom = Instantiate(Explosion, transform.position + new Vector3(0, 1, 0), transform.rotation);
        mineSoundEmmiter.PlayOneShot(mineSounds[1]);
        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);
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
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Mine");
        foreach (GameObject o in gameObjects)
        {
            Vector3 dir = transform.position - o.transform.position;
            float distance = dir.magnitude;
            if (distance < blastRadius)
            {
                o.GetComponent<blowUpMine>().explodeFromOutside();
            }
        }
    }

    public void explodeFromOutside()
    {
        if(!hasExploded)
        {
            Explode();
            Destroy(mineObj);
            Destroy(gameObject, 10f);
        }
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //        hasExploded = true;
    //        Instantiate(Explosion, transform.position, transform.rotation);
    //        transform.SendMessage("DoDamage", mineDamage, SendMessageOptions.DontRequireReceiver);
    //        Destroy(gameObject);
    //}
}
