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

    public Room(int x, int y)
    {
        xCoord = x;
        yCoord = y;

        // TODO Generate room internals here
    }

    // Start is called before the first frame update
    void Start()
    {
        
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

}
