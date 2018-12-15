using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doDamageScrub : MonoBehaviour {
    public GameObject head;
    public int health = 10;
    public GameObject Explosion;
    private bool isKilled = false;
    public AudioClip boomSound;
    private AudioSource headAudio;

    void DoDamage(int damgeAmount)
    {
        health -= damgeAmount;
    }

    // Update is called once per frame
    void Update() {
        if (head == null)
        {
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (head.tag.ToString().Equals("Mine"))
            {
                if (health <= 0)
                {
                    head.GetComponent<blowUpMine>().explodeFromOutside();
                }
            }
            else
            {
                if (headAudio == null)
                    headAudio = head.GetComponent<AudioSource>();

                if (health <= 0 && !isKilled)
                {
                    isKilled = true;

                    headAudio.PlayOneShot(boomSound);
                    GameObject boom = Instantiate(Explosion, head.transform.position + new Vector3(0, 1, 0), head.transform.rotation);

                    Destroy(boom, 2f);
                    foreach (Renderer r in head.GetComponentsInChildren<Renderer>())
                    {
                        if (r != null)
                        {
                            r.enabled = false;
                        }
                    }

                }

                if (isKilled && !headAudio.isPlaying)
                {
                    Destroy(head);
                }
            }
        }
	}
}
