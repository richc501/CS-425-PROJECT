using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour {
    public bool north;
    public bool south;
    public bool east;
    public bool west;
    public GameObject next;

    // Use this for initialization
    void Start () {
		
	}

    void OnTriggerEnter(Collider player) {
        Debug.Log(player.gameObject.name);
        if (player.gameObject.name != "TennisBall")
            return;
        //determine where to spawn
        bool spawned = false;
        Vector3 pos = transform.position;
        while (!spawned){
            int spawn = Random.Range(0, 3);
            switch (spawn) {
                case 0:
                    pos += new Vector3(30, 0, 0);
                    break;
                case 1:
                    pos += new Vector3(-30, 0, 0);
                    break;
                case 2:
                    pos += new Vector3(0, 0, 30);
                    break;
                case 3:
                    pos += new Vector3(0, 0, -30);
                    break;
            }
            spawned = true;
        }
        GameObject lvl = Instantiate(next, pos,new Quaternion())as GameObject;

    }

    // Update is called once per frame
    void Update () {
		
	}
}
