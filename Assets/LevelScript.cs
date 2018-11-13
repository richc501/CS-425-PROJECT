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
        if (CheckAllObj())
        {
            //determine where to spawn

            bool spawned = false;
            Vector3 pos = transform.position;
            int spawn = Random.Range(0, 3);
            while (!spawned)
            {

                switch (spawn)
                {
                    case 0://north
                        pos += new Vector3(30, 0, 0);
                        if (!CheckPos(pos))
                        {
                            spawned = false;

                            spawn = Random.Range(1, 3);
                        }
                        break;
                    case 1://south
                        pos += new Vector3(-30, 0, 0);
                        if (!CheckPos(pos))
                        {
                            spawned = false;

                            do
                            {
                                spawn = Random.Range(0, 3);
                            } while (spawn == 1);
                        }
                        break;
                    case 2://east
                        pos += new Vector3(0, 0, 30);
                        if (!CheckPos(pos))
                        {
                            spawned = false;

                            do
                            {
                                spawn = Random.Range(0, 3);
                            } while (spawn == 2);
                        }
                        break;
                    case 3://west
                        pos += new Vector3(0, 0, -30);
                        if (!CheckPos(pos))
                        {
                            spawned = false;
                            spawn = Random.Range(0, 2);
                        }
                        break;
                }
                if (CheckPos(pos))
                {
                    spawned = true;
                }
                else
                {
                    pos = transform.position;
                }

            }
            GameObject lvl = Instantiate(next, pos, new Quaternion()) as GameObject;
        }
    }

    void OnTriggerEnter(Collider player)
    {
        //Debug.Log(player.gameObject.name);
        //if (player.gameObject.name != "TennisBall")
        //    return;
        //determine where to spawn
        //bool spawned = false;
        //Vector3 pos = transform.position;
        //int spawn = Random.Range(0, 3);
        //while (!spawned)
        //{

        //    switch (spawn)
        //    {
        //        case 0://north
        //            pos += new Vector3(30, 0, 0);
        //            if (!CheckPos(pos))
        //            {
        //                spawned = false;
        //                spawn = Random.Range(1, 3);
        //            }
        //            break;
        //        case 1://south
        //            pos += new Vector3(-30, 0, 0);
        //            if (!CheckPos(pos))
        //            {
        //                spawned = false;
        //                do
        //                {
        //                    spawn = Random.Range(0, 3);
        //                } while (spawn == 1);
        //            }
        //            break;
        //        case 2://east
        //            pos += new Vector3(0, 0, 30);
        //            if (!CheckPos(pos))
        //            {
        //                spawned = false;
        //                do
        //                {
        //                    spawn = Random.Range(0, 3);
        //                } while (spawn == 2);
        //            }
        //            break;
        //        case 3://west
        //            pos += new Vector3(0, 0, -30);
        //            if (!CheckPos(pos))
        //            {
        //                spawned = false;
        //                spawn = Random.Range(0, 2);
        //            }
        //            break;
        //    }
        //    if (CheckPos(pos))
        //    {
        //        spawned = true;
        //    }
        //    else
        //    {
        //        pos = transform.position;
        //    }

        //}
        //GameObject lvl = Instantiate(next, pos, new Quaternion()) as GameObject;

    }

    // Update is called once per frame
    void Update () {
		
	}

    bool CheckPos(Vector3 pos)
    {
        Collider[] hit = Physics.OverlapSphere(pos, 20);
        if (hit.Length == 0)
            return false;
        else
            return true;
    }

    bool CheckAllObj()
    {
        Vector3 origin =  new Vector3(0,0,0);
        Collider[] hit = Physics.OverlapSphere(origin, 10000);
        if(hit.Length>500)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
