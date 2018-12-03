using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript2 : MonoBehaviour {
    
	// Use this for initialization
    //public bool north;
    //public bool south;
    //public bool east;
    //public bool west;
    public bool spawnNorth;
    public bool spawnSouth;
    public bool spawnEast;
    public bool spawnWest;
    public GameObject[] allTiles;
    public static GameObject[][] tiles;//make 2D, tiles[difficulty-level][random]

    private GameObject next;
    private Renderer r;
    private Vector3 size;
    // Use this for initialization
    void Start()
    {
        r = GetComponent<Renderer>();
        size = r.bounds.size;
        Debug.Log("Render Size: " + size);
        if (CheckAllObj())
        {
            //determine where to spawn
            next = tiles[0][Random.Range(0, tiles[0].Length)];
            bool spawned = false;
            Vector3 pos = transform.position;
            int spawn = Random.Range(0, 4);
            size = next.transform.GetComponent<Collider>().bounds.size;
            Debug.Log("Collider Size : " + size);
            while (!spawned)
            {

                switch (spawn)
                {
                    case 0://north
                        if (spawnNorth)
                        {
                            pos += new Vector3(size.x, 0, 0);
                            if (!CheckPos(pos))
                            {
                                spawned = false;

                                spawn = Random.Range(1, 4);
                            }
                            break;
                        }
                        else
                            goto case 1;
                    case 1://south
                        if (spawnSouth)
                        {
                            pos += new Vector3(-size.x, 0, 0);
                            if (!CheckPos(pos))
                            {
                                spawned = false;

                                do
                                {
                                    spawn = Random.Range(0, 4);
                                } while (spawn == 1);
                            }
                            break;
                        }
                        else
                            goto case 2;
                    case 2://east
                        if (spawnEast)
                        {
                            pos += new Vector3(0, 0, size.z);
                            if (!CheckPos(pos))
                            {
                                spawned = false;

                                do
                                {
                                    spawn = Random.Range(0, 4);
                                } while (spawn == 2);
                            }
                            break;
                        }
                        else
                            goto case 3;
                    case 3://west
                        if (spawnWest)
                        {
                            pos += new Vector3(0, 0, -size.z);
                            if (!CheckPos(pos))
                            {
                                spawned = false;
                                spawn = Random.Range(0, 3);
                            }
                            break;
                        }
                        else
                        {
                            Debug.Log("see if I can spwn something somewhere, otherwise pick new tile");
                            break;
                        }
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

    // Update is called once per frame
    void Update()
    {

    }

    public bool spawnNext(int direction)
    {
        return true;
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
        Vector3 origin = new Vector3(0, 0, 0);
        Collider[] hit = Physics.OverlapSphere(origin, 10000);
        if (hit.Length > 500)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}

