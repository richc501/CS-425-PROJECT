using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doDamageScrub : MonoBehaviour {
    public int health = 10;
	
    void DoDamage(int damgeAmount)
    {
        health -= damgeAmount;
    }

    // Update is called once per frame
    void Update () {
		if(health<=0)
        {
            Destroy(gameObject);
        }
	}
}
