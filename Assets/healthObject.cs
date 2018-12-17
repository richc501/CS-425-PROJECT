using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        if (health<=0)
        {
            health = 0;
            healthText.text = ((int)health).ToString();
            dead();
        }
    }

    public void DoHealOnPlayer(int healAmount)
    {
        health += healAmount;
        if(health>(health+overHealth))
        {
            health = 100 + overHealth;
        }
        healthText.text = ((int)health).ToString();
    }

    void dead()
    {
        //make player die
        SceneManager.LoadScene("dead");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

}
