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
    private const int max = 30;//max tiles to spawn till goal is next
    public GameObject goal;

    //defines a path with its tendecies to go one direction
    class Path
    {
        public Vector3 pos;
        public int min;
        public int max;
        public int[] exclusions;

        public Path(Vector3 pos, int min, int max, int[] exclusions)
        {
            this.pos = pos;
            this.min = min;
            this.max = max;
            this.exclusions = exclusions;
        }

        public void SetPos(Vector3 pos)
        {
            this.pos = pos;
        }
    }

    // Use this for initialization
    void Start()
    {
        //make 2D array of size[numOfLevels][determined at runtime]
        tiles = new GameObject[folders.Length][];

        //load the tiles into the correct arrays
        for (int i = 0; i < folders.Length; i++)
            tiles[i] = Resources.LoadAll(folders[i], typeof(GameObject)).Cast<GameObject>().ToArray();

        List<Path> positions = new List<Path>();
        //add the default path, tending towards east
        positions.Add(new Path(transform.position, 0, 3, new int[0]));
        int numSpawned = 0;
        while (numSpawned <= max + 1)
        {
            for (int i = 0; i < positions.Count; i++)
                if (i == 0 && Random.Range(1, 100) < 10)
                    positions.Add(new Path(spawnNext(positions[i], numSpawned), 0, 4, new int[2] { 0, 2 }));
                else
                    positions[i].SetPos(spawnNext(positions[i], numSpawned));

            numSpawned++;
        }
    }

    Vector3 spawnNext(Path p, int numSpawned)
    {
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
        int i = 0;
        while (!spawned)
        {
            //determine where to spawn based on the direction
            switch (spawn)
            {
                case 0://north
                    Debug.Log("0");
                    if (empty(pos + new Vector3(300, 0, 0))){
                        pos += new Vector3(300, 0, 0);
                        spawned = true;
                        
                        Debug.Log("Empty");
                    }
                    else
                        spawn = Random.Range(1, 4);
                    break;
                case 1://south
                    Debug.Log("1");
                    if (empty(pos + new Vector3(-300, 0, 0))){
                        pos += new Vector3(-300, 0, 0);
                        spawned = true;
                    }
                    else
                        do{
                            spawn = Random.Range(0, 4);
                        } while (spawn == 1);
                    break;

                case 2://east
                    Debug.Log("2");
                    if (empty(pos + new Vector3(0, 0, 300))){
                        pos += new Vector3(0, 0, 300);
                        spawned = true;

                    }else
                        do{
                            spawn = Random.Range(0, 4);
                        } while (spawn == 2);

                    break;

                case 3://west
                    Debug.Log("3");
                    if (empty(pos + new Vector3(0, 0, -300))){
                        pos += new Vector3(0, 0, -300);
                        spawned = true;
                    }else
                        spawn = Random.Range(0, 3);
                    break;

            }
            if (i++ == 10)
                break;
        }
        Instantiate(next, pos, new Quaternion());
        //set the new position for the path
        p.pos = pos;
        return pos;
    }

    //check if the given position is empty, if it is return false;
    bool empty(Vector3 pos)
    {
        Collider[] hit = Physics.OverlapSphere(pos, 149);
        //if we didn't hit anything return true;
        if (hit.Length == 0)
            return true;
        else { 
            Debug.Log("Position: " + pos.ToString() + "Occupied");
            return false;
        }
            
    }

}