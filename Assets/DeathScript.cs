using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DeathScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update () {
        if (transform.position.y < -20)
        {
            SceneManager.LoadScene("dead");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

        }
    }
}
