using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthObject : MonoBehaviour {
    public float health = 100;
    public int overHealth = 50;
    public RectTransform healthTransform;
    private Text healthText;
	// Use this for initialization
	void Start () {
        healthText = healthTransform.GetComponent<Text>();

	}

    void LateUpdate()
    {
        if(health>100)
        {
            health -= Time.deltaTime;
            if(health<100)
            {
                health = 100;
            }
            healthText.text = ((int)health).ToString();
        }
    }

    public void DoDamageOnPlayer(int damageAmount)
    {
        health -= damageAmount;
        healthText.text = ((int)health).ToString();
        if (health<0)
        {
            health = 1;
            dead();
        }
    }

    public void DoHealOnPlayer(int healAmount)
    {
        health += healAmount;
        healthText.text = ((int)health).ToString();
    }

    void dead()
    {
        //make player die
    }

}
