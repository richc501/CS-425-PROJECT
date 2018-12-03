using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class SpawningScript : MonoBehaviour
{
    //only used if a tile is made such that it can only spawn the next level on one side
    public bool north;
    public bool south;
    public bool east;
    public bool west;
    //for determining what difficulty of levels to spawn
    public int difficulty;
    //set in the editor, contains the paths for all different levels
    public string[] folders;
    //used for picking a random tile
    private GameObject[][] tiles;//make 2D, tiles[difficulty-level][random]
    private GameObject next;//next tile to spawn
    private const int max = 100;//max tiles to spawn till goal is next
    public GameObject goal;

    //defines a path with its tendecies to go one direction
    class Path {
        public Vector3 pos;
        public int min;
        public int max;
        public int[] exclusions;

        public Path(Vector3 pos, int min, int max,int[] exclusions) {
            this.pos = pos;
            this.min = min;
            this.max = max;
            this.exclusions = exclusions;
        }

        public void SetPos(Vector3 pos) {
            this.pos = pos;
        }
    }

    // Use this for initialization
    void Start(){
        //make 2D array of size[numOfLevels][determined at runtime]
        tiles = new GameObject[folders.Length][];

        //load the tiles into the correct arrays
        for(int i = 0; i < folders.Length; i++)
            tiles[i] = Resources.LoadAll(folders[i], typeof(GameObject)).Cast<GameObject>().ToArray();

        List<Path> positions = new List<Path>();
        //add the default path, tending towards east
        positions.Add(new Path(transform.position,0,3,new int[0]));
        int numSpawned = 0;
        while (numSpawned <= max + 1)
        {
            for (int i = 0; i < positions.Count; i++)
                if (i == 0 && Random.Range(1, 100) < 10)
                    positions.Add(new Path(spawnNext(positions[i], numSpawned), 0, 4, new int[2]{0,2}));
                else
                    positions[i].SetPos(spawnNext(positions[i], numSpawned));
            
            numSpawned++;
        }
    }

    Vector3 spawnNext(Path p, int numSpawned) {
        Vector3 pos = p.pos;
        if (numSpawned == max + 1)
            next = goal;
        else
            next = tiles[difficulty][Random.Range(0, tiles[difficulty].Length)];
        //determine where to spawn
        int spawn = 0;
        Vector3 size = next.GetComponent<Renderer>().bounds.size;
        //Debug.Log(size);
        do
        {
            spawn = Random.Range(p.min, p.max);
        } while (p.exclusions.Contains<int>(spawn));
        bool spawned = false;
        while (!spawned)
        {
            float currentSize = 0;
            //determine where to spawn based on the direction
            switch (spawn)
            {
                case 0://north
                    pos += new Vector3(size.x, 0, 0);
                    if (!CheckPos(pos, size.x))
                    {
                        spawned = false;

                        spawn = Random.Range(1, 4);
                    }
                    currentSize = size.x;
                    break;
                case 1://south
                    pos += new Vector3(-size.x, 0, 0);
                    if (!CheckPos(pos, size.x))
                    {
                        spawned = false;

                        do
                        {
                            spawn = Random.Range(0, 4);
                        } while (spawn == 1);
                    }
                    currentSize = size.x;
                    break;

                case 2://east
                    pos += new Vector3(0, 0, size.z);
                    if (!CheckPos(pos, size.z))
                    {
                        spawned = false;

                        do
                        {
                            spawn = Random.Range(0, 4);
                        } while (spawn == 2);
                    }
                    currentSize = size.z;
                    break;

                case 3://west
                    pos += new Vector3(0, 0, -size.z);
                    if (!CheckPos(pos, size.z))
                    {
                        spawned = false;
                        spawn = Random.Range(0, 3);
                    }
                    currentSize = size.z;
                    break;

            }
            if (CheckPos(pos, currentSize))
            {
                spawned = true;
            }


        }
        GameObject lvl = Instantiate(next, pos, new Quaternion()) as GameObject;
        //set the new position for the path
        p.pos = pos;
        return pos;
    }

    //check if the given position is empty, if it is return false;
    bool CheckPos(Vector3 pos, float size)
    {
        Collider[] hit = Physics.OverlapSphere(pos, size);
        //if we didn't hit anything return true;
        if (hit.Length == 0)
            return false;
        else
            return true;
    }

}