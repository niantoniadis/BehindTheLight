using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public Room sampleRoom;
    public Room playerRoom; // the room the player is currently inside
    public List<Room> allRooms;
    public Room[] loadedRooms;
    public GameObject mainCamera; // the main camera, needs to follow the playerRoom

    void Start()
    {
        allRooms = new List<Room>();
        loadedRooms = new Room[5];

        CreateRoom(0, 0);
        //CreateRoom(0, 1);
        //CreateRoom(0, -1);
        //CreateRoom(1, 0);
        //CreateRoom(-1, 0);
        //DrawCurrentRoom();
    }

    void Update()
    {
        
    }

    void UpdateMap()
    {
        //unload old rooms
        //load new rooms
    }

    void LoadRooms()
    {

    }

    void UnloadRooms()
    {

    }

    public void CreateRoom(int x, int y)
    {
        Room r = Instantiate(sampleRoom.gameObject, Vector3.zero, Quaternion.identity).GetComponent<Room>();
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
