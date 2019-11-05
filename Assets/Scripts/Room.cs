using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    private Room topRoom;
    private Room leftRoom;
    private Room rightRoom;
    private Room bottomRoom;
    float xBound = 8.886f;
    float yBound = 5f;

    Vector3 center;

    public int xCoord;
    public int yCoord;

    public EnemySpawner spawner;
    List<EnemySpawner> spawners = new List<EnemySpawner>();
    public GameObject background;

    public void Load(int x, int y)
    {
        xCoord = x;
        yCoord = y;

        center = new Vector3(x * xBound, y * yBound, 0);
        background = Instantiate(background.gameObject, center, Quaternion.identity);
        SpawnSpawners();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 Center
    {
        get
        {
            return center;
        }
    }

    public bool PlayerInRoom(Player coords)
    {
        // check if player is in this room
        return true;
    }

    public bool MatchRoom(int x, int y)
    {
        return x == xCoord && y == yCoord;
    }

    // Sets neighbor n. n = 1 for top, 2 for left, 3 for right, 4 for bottom
    public void SetNeighbor(int n, Room neighbor)
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

    public void ActivateSpawners()
    {
        foreach(EnemySpawner spawner in spawners)
        {
            spawner.Active = true;
        }
    }

    public void DeactivateSpawners()
    {
        foreach (EnemySpawner spawner in spawners)
        {
            spawner.Active = false;
        }
    }

    public void Walls(Vehicle vehicle)
    {
        if(Mathf.Abs(vehicle.Position.x) >= xBound || Mathf.Abs(vehicle.Position.y) >= yBound)
        {
            vehicle.ApplyForce(Vector3.zero - vehicle.Position);
        }
    }

    public float GetXMax()
    {
        return center.x + 8.886f;
    }

    public float GetXMin()
    {
        return center.x - 8.886f;
    }
    public float GetYMax()
    {
        return center.y + 5f;
    }

    public float GetYMin()
    {
        return center.y - 5f;
    }
}
