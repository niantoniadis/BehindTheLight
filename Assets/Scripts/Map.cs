using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public List<Room> rooms = new List<Room>();
    public Room currentRoom;
    public GameObject testTile;
    public GameObject mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        rooms.Add(new Room(0, 0));
        rooms.Add(new Room(0, 1));
        rooms.Add(new Room(1, 0));
        rooms.Add(new Room(-1, 0));
        rooms.Add(new Room(0, -1));
        DrawCurrentRoom();
    }

    // Update is called once per frame
    void Update()
    {
        // Draw the current room and its neighbors
        //DrawCurrentRoom();

        // lock main camera to the current room
        
    }

    // Returns a link to the room at the requested coordinates. If the room doesn't yet exist, it is created.
    public Room LoadRoom(int xCoord, int yCoord)
    {
        // Check if the requested room's coords have been loaded before. If so, return link to that room.
        foreach (Room room in rooms)
        {
            if (room.MatchRoom(xCoord, yCoord))
            {
                return room;
            }
        }

        // Generate a new room
        Room newRoom = new Room(xCoord, yCoord);
        rooms.Add(newRoom);
        return newRoom;
    }

    public void DrawCurrentRoom()
    {
        // load all rooms into the world
        foreach (Room room in rooms)
        {
            Instantiate(testTile, new Vector3(room.xCoord, room.yCoord, 0), Quaternion.identity);
        }
        
    }

}
