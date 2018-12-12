using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthObject : MonoBehaviour {
    public int health = 100;
    public int overHealth = 50;
    public RectTransform healthTransform;
    private Text healthText;
	// Use this for initialization
	void Start () {
        healthText = healthTransform.GetComponent<Text>();

	}

    void LateUpdate()
    {
        if(health>(health+overHealth))
        {
            health--;
            healthText.text = health.ToString();
        }
    }

    public void DoDamageOnPlayer(int damageAmount)
    {
        health -= damageAmount;
        healthText.text = health.ToString();
        if (health<0)
        {
            health = 1;
            dead();
        }
    }

    public void DoHealOnPlayer(int healAmount)
    {
        health += healAmount;
        healthText.text = health.ToString();
    }

    void dead()
    {
        //make player die
    }

}
