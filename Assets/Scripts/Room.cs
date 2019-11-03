using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    private Room topRoom;
    private Room leftRoom;
    private Room rightRoom;
    private Room bottomRoom;

    public int xCoord;
    public int yCoord;

    public void InstantiateRoom(int x, int y)
    {
        xCoord = x;
        yCoord = y;

        // TODO Generate room internals here
    }
        
    public EnemySpawner spawner;
    public List<EnemySpawner> spawners;
    public GameObject background;
    // Start is called before the first frame update
    void Start()
    {
        spawners = new List<EnemySpawner>();
        background = Instantiate(background.gameObject, background.transform.position, Quaternion.identity);
        SpawnSpawners();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool MatchRoom(int x, int y)
    {
        return x == xCoord && y == yCoord;
    }

    // Sets neighbor n. n = 1 for top, 2 for left, 3 for right, 4 for bottom
    public void setNeighbor(int n, Room neighbor)
    {
        if (n == 1)
        {
            topRoom = neighbor;
        }
        else if (n == 2)
        {
            leftRoom = neighbor;
        }
        else if (n == 3)
        {
            rightRoom = neighbor;
        }
        else if (n == 4)
        {
            bottomRoom = neighbor;
        }
    }
    public void SpawnSpawners()
    {
        float right = background.GetComponent<SpriteRenderer>().bounds.max.x;
        float left = background.GetComponent<SpriteRenderer>().bounds.min.x;
        float top = background.GetComponent<SpriteRenderer>().bounds.max.y;
        float bottom = background.GetComponent<SpriteRenderer>().bounds.min.y;
        spawners.Add(Instantiate(spawner.gameObject, new Vector3(0.9f * right, top + bottom, 0), Quaternion.identity).GetComponent<EnemySpawner>());
        spawners.Add(Instantiate(spawner.gameObject, new Vector3(0.9f * left, top + bottom, 0), Quaternion.identity).GetComponent<EnemySpawner>());
        spawners.Add(Instantiate(spawner.gameObject, new Vector3(right + left, 0.9f * top, 0), Quaternion.identity).GetComponent<EnemySpawner>());
        spawners.Add(Instantiate(spawner.gameObject, new Vector3(right + left, 0.9f * bottom, 0), Quaternion.identity).GetComponent<EnemySpawner>());
    }

    public List<EnemySpawner> GetSpawners()
    {
        return spawners;
    }

}
