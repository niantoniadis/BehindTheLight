using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public Room room;
    public List<Room> rooms = new List<Room>();
    public Room currentRoom;
    public GameObject testTile;
    public GameObject mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        CreateRoom(0, 0);
        //CreateRoom(0, 1);
        //CreateRoom(0, -1);
        //CreateRoom(1, 0);
        //CreateRoom(-1, 0);
        //DrawCurrentRoom();
    }

    // Update is called once per frame
    void Update()
    {
        // Draw the current room and its neighbors
        //DrawCurrentRoom();

        // lock main camera to the current room
        
    }

    public void CreateRoom(int x, int y)
    {
        Room r = Instantiate(room.gameObject, Vector3.zero, Quaternion.identity).GetComponent<Room>();
        r.InstantiateRoom(x, y);
        rooms.Add(r);
    }

    public List<Room> GetRooms()
    {
        return rooms;
    }

    // Returns a link to the room at the requested coordinates. If the room doesn't yet exist, it is created.
    public void LoadRoom(int xCoord, int yCoord)
    {
        // Check if the requested room's coords have been loaded before. If so, return link to that room.
        foreach (Room room in rooms)
        {
            if (room.MatchRoom(xCoord, yCoord))
            {
                
            }
        }

        // Generate a new room
        CreateRoom(xCoord, yCoord);
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
